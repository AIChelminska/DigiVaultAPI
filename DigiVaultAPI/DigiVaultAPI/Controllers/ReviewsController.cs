using DigiVaultAPI.Features.Review.Messages.Commands;
using DigiVaultAPI.Features.Review.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Route("api/courses/{idCourse}/reviews")]
public class ReviewsController (IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReviewById([FromRoute] int idCourse)
        => Ok(await mediator.Send(new GetReviewByIdQuery { IdCourse = idCourse }));

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddReview([FromRoute] int idCourse, [FromBody] AddReviewCommand request)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser")!);
        request.IdUser = idUser;
        request.IdCourse = idCourse;
        await mediator.Send(request);
        return StatusCode(201);
    }

    [Authorize]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReview([FromRoute] int idCourse)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser")!);
        await mediator.Send(new DeleteReviewCommand { IdUser = idUser, IdCourse = idCourse });
        return NoContent();
    }
}