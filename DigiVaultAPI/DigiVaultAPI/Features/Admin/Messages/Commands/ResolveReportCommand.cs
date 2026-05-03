using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class ResolveReportCommand : IRequest
{
    public int IdCourseReport { get; set; }
}