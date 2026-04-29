using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand>
{
    private readonly IAdminService _adminService;

    public DeleteCourseHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
    {
        await _adminService.DeleteCourse(command.IdCourse);
    }
}