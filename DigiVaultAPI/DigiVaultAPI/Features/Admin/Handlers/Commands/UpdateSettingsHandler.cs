using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class UpdateSettingsHandler : IRequestHandler<UpdateSettingsCommand>
{
    private readonly IAdminService _adminService;

    public UpdateSettingsHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(UpdateSettingsCommand request, CancellationToken cancellationToken)
    {
        await _adminService.UpdateSettings(request.CommissionRate);
    }
}