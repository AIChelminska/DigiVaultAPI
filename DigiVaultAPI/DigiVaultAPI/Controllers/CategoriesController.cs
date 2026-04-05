using DigiVaultAPI.Features.Categories.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesQuery query)
        => Ok(await mediator.Send(query));
}