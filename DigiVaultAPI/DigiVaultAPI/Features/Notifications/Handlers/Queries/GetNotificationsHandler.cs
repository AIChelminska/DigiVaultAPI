using DigiVaultAPI.Features.Notifications.Messages.Queries;
using DigiVaultAPI.Features.Notifications.Providers;
using DigiVaultAPI.Features.Notifications.Messages.DTOs;
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
        return await _provider.GetNotifications(query.IdUser);
    }
}