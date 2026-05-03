using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class ResolveReportHandler : IRequestHandler<ResolveReportCommand>
{
    private readonly IAdminService _service;

    public ResolveReportHandler(IAdminService service)
    {
        _service = service;
    }

    public async Task Handle(ResolveReportCommand command, CancellationToken cancellationToken)
    {
        await _service.ResolveReport(command.IdCourseReport);
    }
}