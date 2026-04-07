using DigiVaultAPI.Features.Notifications.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Notifications.Messages.Queries;

public class GetNotificationsQuery : IRequest<IEnumerable<NotificationDto>>
{
    public int IdUser { get; set; }
}