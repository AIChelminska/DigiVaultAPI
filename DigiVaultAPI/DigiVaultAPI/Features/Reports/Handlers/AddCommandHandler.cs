using DigiVaultAPI.Features.Reports.Messages.Commands;
using DigiVaultAPI.Features.Reports.Services;
using MediatR;

namespace DigiVaultAPI.Features.Reports.Handlers;

public class AddCommandHandler : IRequestHandler<AddReportCommand>
{
    private readonly IReportService _reportService;

    public AddCommandHandler(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task Handle(AddReportCommand command, CancellationToken cancellationToken)
    {
        await _reportService.AddReport(command.IdUser, command.IdCourse, command.Reason);
    }
}