using MediatR;

namespace DigiVaultAPI.Features.Notifications.Messages.Commands;

public class SetAsReadedNotificationCommand : IRequest
{
    public int IdNotification { get; set; }
    public int IdUser { get; set; }
}