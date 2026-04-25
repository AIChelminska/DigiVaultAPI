using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IAdminService _adminService;

    public UpdateCategoryHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        await _adminService.UpdateCategory(command.IdCategory, command.Name);
    }
}