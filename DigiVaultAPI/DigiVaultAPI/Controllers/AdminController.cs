using DigiVaultAPI.Features.Admin.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SetAsActiveUser([FromBody] SetAsActiveUserCommand command)
        => Ok(await mediator.Send(command));
}



