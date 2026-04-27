using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IAdminService _adminService;

    public UpdateUserHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }
    
    public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        await _adminService.UpdateUser(command.IdUser, command.Role, command.WarningsCount, command.IsActive);
    }
}