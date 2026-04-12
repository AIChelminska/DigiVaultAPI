using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Providers;
using DigiVaultAPI.Features.Courses.Messages.DTOs;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, PagedResult<AdminOrdersDto>>
{
    private readonly IAdminProvider _provider;

    public GetOrdersHandler(IAdminProvider provider)
    {
        _provider = provider;
    }

    public async Task<PagedResult<AdminOrdersDto>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await _provider.GetOrders(query.Page, query.PageSize, query.Search, query.DateFrom, query.DateTo);
        var total = await _provider.GetOrdersCount(query.Search, query.DateFrom, query.DateTo);

        return new PagedResult<AdminOrdersDto>
        {
            Items = orders.Adapt<List<AdminOrdersDto>>(),
            Total = total,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalPages = (int)Math.Ceiling(total / (double)query.PageSize)
        };
    }
}
