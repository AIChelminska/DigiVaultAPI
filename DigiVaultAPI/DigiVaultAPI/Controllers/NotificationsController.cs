using DigiVaultAPI.Features.Notifications.Messages.Queries;
using DigiVaultAPI.Features.Notifications.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotifications()
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var query = new GetNotificationsQuery { IdUser = idUser };
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpPatch("{idNotification}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SetAsReadedNotification([FromRoute] int idNotification)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var command = new SetAsReadedNotificationCommand { IdNotification = idNotification, IdUser = idUser };
        await mediator.Send(command);
        return NoContent();
    }
}