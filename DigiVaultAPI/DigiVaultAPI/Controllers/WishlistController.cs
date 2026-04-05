using MediatR;
using Microsoft.AspNetCore.Mvc;
using DigiVaultAPI.Features.Wishlist.Messages.Queries;
using DigiVaultAPI.Features.Wishlist.Messages.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WishlistController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWishlist()
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var query = new GetWishlistQuery { IdUser = idUser};
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [Authorize]
    [HttpDelete("{idCourse}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveFromWishlist(int idCourse)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var command = new RemoveFromWishlistCommand { IdUser = idUser, IdCourse = idCourse };
        await mediator.Send(command);
        return NoContent();
    }
    
    [Authorize]
    [HttpPost("{idCourse}")]
    public async Task<IActionResult> AddToWishlist(int idCourse)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser"));
        var command = new AddToWishlistCommand { IdUser = idUser, IdCourse = idCourse };
        await mediator.Send(command);
        return Created();
    }
}