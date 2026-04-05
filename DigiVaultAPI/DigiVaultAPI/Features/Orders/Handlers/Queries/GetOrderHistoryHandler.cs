using DigiVaultAPI.Features.Orders.Messages.Queries;
using DigiVaultAPI.Features.Orders.Providers;
using DigiVaultAPI.Features.Orders.Messages.DTOs;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Orders.Handlers.Queries;

public class GetOrderHistoryHandler : IRequestHandler<GetOrderHistoryQuery, List<OrderHistoryDto>>
{
    private readonly IOrderProvider _provider;

    public GetOrderHistoryHandler(IOrderProvider provider)
    {
        _provider = provider;
    }

    public async Task<List<OrderHistoryDto>> Handle(GetOrderHistoryQuery query, CancellationToken cancellationToken)
    {
        var orders = await _provider.GetOrders(query.IdUser);
        var dtos = orders.Adapt<List<OrderHistoryDto>>();
        return dtos;
    }

    
}

