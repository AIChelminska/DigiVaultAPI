using DigiVaultAPI.Features.Orders.Messages.Queries;
using DigiVaultAPI.Features.Orders.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrderHistory()
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var query = new GetOrderHistoryQuery { IdUser = idUser };
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{idOrder}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetOrderDetails([FromRoute] int idOrder)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var query = new GetOrderDetailsQuery { IdOrder = idOrder, IdUser = idUser };
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateOrder()
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var command = new CreateOrderCommand { IdUser = idUser };
        var idOrder = await mediator.Send(command);
        var details = await mediator.Send(new GetOrderDetailsQuery { IdOrder = idOrder, IdUser = idUser });
        return CreatedAtAction(nameof(GetOrderDetails), new { idOrder }, details);
    }
}