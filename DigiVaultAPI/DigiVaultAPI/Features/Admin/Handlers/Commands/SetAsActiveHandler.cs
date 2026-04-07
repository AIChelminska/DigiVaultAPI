using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class SetAsActiveHandler : IRequestHandler<SetAsActiveUserCommand>
{
    private readonly IAdminService _adminService;

    public SetAsActiveHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(SetAsActiveUserCommand command, CancellationToken cancellationToken)
    {
        await _adminService.SetAsActiveUser(command.IdUser);
    }
}