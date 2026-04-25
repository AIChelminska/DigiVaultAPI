using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand>
{
    private readonly IAdminService _adminService;

    public CreateCategoryHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        await _adminService.CreateCategory(command.Name);
    }
}