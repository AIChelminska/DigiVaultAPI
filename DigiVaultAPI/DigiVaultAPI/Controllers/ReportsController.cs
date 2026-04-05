using DigiVaultAPI.Features.Reports.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DigiVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController(IMediator mediator) : ControllerBase
{
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddReport([FromBody] AddReportCommand command)
    {
        var idUser = int.Parse(User.FindFirstValue("IdUser")!);
        command.IdUser = idUser;
        await mediator.Send(command);
        return StatusCode(201);
    }
}