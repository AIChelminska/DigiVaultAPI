using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DigiVaultAPI.Features.Courses.Messages.Queries;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Authorize(Roles = "Worker")]
[Route("api/[controller]")]
public class AdminController(IMediator mediator) : ControllerBase
{
    [HttpGet("users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers()
        => Ok(await mediator.Send(new GetUsersQuery()));

    [HttpPost("users/set-as-active")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SetAsActiveUser([FromBody] SetAsActiveUserCommand command)
        { await mediator.Send(command); return NoContent(); }

    [HttpPost("users/set-as-not-active")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SetAsNotActiveUser([FromBody] SetAsNotActiveCommand command)
        { await mediator.Send(command); return NoContent(); }

    [HttpGet("courses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCourses([FromQuery] GetCoursesQuery query)
    => Ok(await mediator.Send(query));    

    [HttpGet("orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQuery query)
    => Ok(await mediator.Send(query));

    [HttpGet("categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesQuery query)
    => Ok(await mediator.Send(query));


}



