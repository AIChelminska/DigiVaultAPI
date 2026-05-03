using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using DigiVaultAPI.Models;
using MediatR;
using DigiVaultAPI.Exceptions;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand>
{
    private readonly IAdminService _adminService;

    public CreateNotificationHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(CreateNotificationCommand command, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            IdUser = command.IdUser,
            Title = command.Title,
            Message = command.Message,
        };
        await _adminService.CreateNotification(notification);
    }
}