using DigiVaultAPI.Features.Notifications.Messages.DTOs;
using DigiVaultAPI.Features.Notifications.Messages.Queries;
using DigiVaultAPI.Features.Notifications.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Notifications.Handlers.Queries;

public class GetNotificationsHandler : IRequestHandler<GetNotificationsQuery, IEnumerable<NotificationDto>>
{
    private readonly INotificationProvider _provider;

    public GetNotificationsHandler(INotificationProvider provider)
    {
        _provider = provider;
    }

    public async Task<IEnumerable<NotificationDto>> Handle(GetNotificationsQuery query, CancellationToken cancellationToken)
    {
        var notifications = await _provider.GetNotifications(query.IdUser);
        return notifications.Adapt<IEnumerable<NotificationDto>>();
    }
}
