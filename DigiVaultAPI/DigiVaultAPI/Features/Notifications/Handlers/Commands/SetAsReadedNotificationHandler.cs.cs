using DigiVaultAPI.Features.Notifications.Messages.Commands;
using DigiVaultAPI.Features.Notifications.Services;
using MediatR;

namespace DigiVaultAPI.Features.Notifications.Handlers.Commands;

public class SetAsReadedNotificationHandler : IRequestHandler<SetAsReadedNotificationCommand>
{
    private readonly INotificationService _notificationService;

    public SetAsReadedNotificationHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(SetAsReadedNotificationCommand command, CancellationToken cancellationToken)
    {
        await _notificationService.SetAsReadedNotification(command.IdNotification, command.IdUser);
    }
}