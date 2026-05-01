using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Providers;
using DigiVaultAPI.Features.Courses.Messages.DTOs;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetNotificationsAdminHandler : IRequestHandler<GetNotificationsAdminQuery, PagedResult<AdminNotificationDto>>
{
    private readonly IAdminProvider _adminProvider;

    public GetNotificationsAdminHandler(IAdminProvider adminProvider)
    {
        _adminProvider = adminProvider;
    }

    public async Task<PagedResult<AdminNotificationDto>> Handle(GetNotificationsAdminQuery query, CancellationToken cancellationToken)
    {
        var notifications = await _adminProvider.GetNotificationsAdmin(query.Page, query.PageSize, query.Search, query.IsRead);
        var total = await _adminProvider.GetNotificationsAdminCount(query.Search, query.IsRead);

        return new PagedResult<AdminNotificationDto>
        {
            Items = notifications.Adapt<List<AdminNotificationDto>>(),
            Total = total,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalPages = (int)Math.Ceiling(total / (double)query.PageSize)
        };
    }
}