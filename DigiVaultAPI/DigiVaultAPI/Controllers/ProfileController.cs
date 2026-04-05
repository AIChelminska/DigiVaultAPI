using DigiVaultAPI.Features.Profile.Messages.Queries;
using DigiVaultAPI.Features.Profile.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace DigiVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfileController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProfile()
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var query = new GetProfileQuery { IdUser = idUser };
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpPatch("name")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateName([FromBody] UpdateNameCommand request)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        request.IdUser = idUser;
        await mediator.Send(request);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailCommand request)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        request.IdUser = idUser;
        await mediator.Send(request);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommand request)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        request.IdUser = idUser;
        await mediator.Send(request);
        return NoContent();
    }
}
