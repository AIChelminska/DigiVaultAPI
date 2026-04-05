using MediatR;
using Microsoft.AspNetCore.Mvc;
using DigiVaultAPI.Features.Cart.Messages.Queries;
using DigiVaultAPI.Features.Cart.Messages.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCart()
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var query = new GetCartQuery { IdUser = idUser};
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [Authorize]
    [HttpDelete("{idCourse}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveFromCart(int idCourse)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var command = new RemoveFromCartCommand { IdUser = idUser, IdCourse = idCourse };
        await mediator.Send(command);
        return NoContent();
    }
    
    [Authorize]
    [HttpPost("{idCourse}")]
    public async Task<IActionResult> AddToCart(int idCourse)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var command = new AddToCartCommand { IdUser = idUser, IdCourse = idCourse };
        await mediator.Send(command);
        return Created();
    }
}

