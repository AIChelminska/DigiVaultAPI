using DigiVaultAPI.Features.Cms.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CmsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContents()
        => Ok(await mediator.Send(new GetCmsContentsQuery()));

    [HttpGet("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContentByKey([FromRoute] string key)
        => Ok(await mediator.Send(new GetCmsContentByKeyQuery { Key = key }));
}
