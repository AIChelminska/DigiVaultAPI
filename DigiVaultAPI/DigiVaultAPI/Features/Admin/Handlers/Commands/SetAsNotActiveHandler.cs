using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class SetAsNotActiveHandler : IRequestHandler<SetAsNotActiveCommand>
{
    private readonly IAdminService _adminService;

    public SetAsNotActiveHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(SetAsNotActiveCommand command, CancellationToken cancellationToken)
    {
        await _adminService.SetAsNotActiveUser(command.IdUser);
    }
}