using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class RemoveCourseFromOrderHandler : IRequestHandler<RemoveCourseFromOrder>
{
    private readonly IAdminService _adminService;

    public RemoveCourseFromOrderHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(RemoveCourseFromOrder command, CancellationToken cancellationToken)
    {
        await _adminService.RemoveCourseFromOrder(command.IdOrder, command.IdCourse);
    }
}