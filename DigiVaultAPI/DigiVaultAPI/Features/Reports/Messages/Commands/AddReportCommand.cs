using MediatR;

namespace DigiVaultAPI.Features.Reports.Messages.Commands;

public class AddReportCommand : IRequest
{
    public int IdUser { get; set; }
    public int IdCourse { get; set; }
    public string Reason { get; set; } = string.Empty;
}