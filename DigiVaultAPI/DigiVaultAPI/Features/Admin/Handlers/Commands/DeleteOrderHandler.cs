using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IAdminService _adminService;

    public DeleteOrderHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        await _adminService.DeleteOrder(command.IdOrder);
    }
}