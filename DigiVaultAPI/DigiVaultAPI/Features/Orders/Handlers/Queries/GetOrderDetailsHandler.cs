using DigiVaultAPI.Features.Orders.Messages.Queries;
using DigiVaultAPI.Features.Orders.Providers;
using DigiVaultAPI.Features.Orders.Services;
using DigiVaultAPI.Features.Orders.Messages.DTOs;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Orders.Handlers.Queries;

public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, OrderHistoryDto>
{
    private readonly IOrderProvider _provider;
    private readonly IOrderService _service;

    public GetOrderDetailsHandler(IOrderProvider provider, IOrderService service)
    {
        _provider = provider;
        _service = service;
    }

    public async Task<OrderHistoryDto> Handle(GetOrderDetailsQuery query, CancellationToken cancellationToken)
    {
        var order = await _provider.GetOrderById(query.IdOrder);
        _service.EnsureOrderExists(order);
        _service.EnsureOrderBelongsToUser(order!, query.IdUser);
        return order!.Adapt<OrderHistoryDto>();
    }
}