using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IAdminService _adminService;

    public CreateUserHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        await _adminService.CreateUser(command.Login, command.Email, command.Password, command.FirstName, command.LastName, command.Role);
    }
}