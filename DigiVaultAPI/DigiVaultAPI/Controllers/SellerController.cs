using DigiVaultAPI.Features.Courses.Messages.Commands;
using DigiVaultAPI.Features.Courses.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Worker")]
public class SellerController(IMediator mediator) : ControllerBase
{
    [HttpGet("courses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyCourses([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var idUser = int.Parse(User.FindFirst("IdUser")!.Value);
        return Ok(await mediator.Send(new GetSellerCoursesQuery { IdUser = idUser, Page = page, PageSize = pageSize }));
    }

    [HttpPost("courses")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
    {
        command.IdUser = int.Parse(User.FindFirst("IdUser")!.Value);
        var idCourse = await mediator.Send(command);
        return StatusCode(201, new { idCourse });
    }

    [HttpPut("courses/{idCourse}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCourse([FromRoute] int idCourse, [FromBody] UpdateCourseCommand command)
    {
        command.IdCourse = idCourse;
        command.IdUser   = int.Parse(User.FindFirst("IdUser")!.Value);
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("courses/{idCourse}/visibility")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ToggleVisibility([FromRoute] int idCourse, [FromBody] ToggleVisibilityCommand command)
    {
        command.IdCourse = idCourse;
        command.IdUser   = int.Parse(User.FindFirst("IdUser")!.Value);
        await mediator.Send(command);
        return Ok();
    }
}