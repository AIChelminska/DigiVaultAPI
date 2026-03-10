using DigiVaultAPI.Features.Courses.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCourses([FromQuery] GetCoursesQuery query)
        => Ok(await mediator.Send(query));
    

    [HttpGet("popular")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPopular()
        => Ok(await mediator.Send(new GetPopularCoursesQuery()));

    [HttpGet("newest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNewest()
        => Ok(await mediator.Send(new GetNewestCoursesQuery()));

    [HttpGet("top-rated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTopRated()
        => Ok(await mediator.Send(new GetTopRatedCoursesQuery()));

    [HttpGet("{idCourse}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int idCourse)
        => Ok(await mediator.Send(new GetCourseByIdQuery { IdCourse = idCourse }));
}
