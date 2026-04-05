using DigiVaultAPI.Exceptions;
using DigiVaultAPI.Features.Orders.Messages.Queries;
using DigiVaultAPI.Features.Orders.Providers;
using DigiVaultAPI.Features.Orders.Messages.DTOs;
using Mapster;
using MediatR;
namespace DigiVaultAPI.Features.Orders.Handlers.Queries;

public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, OrderHistoryDto>
{
    private readonly IOrderProvider _provider;

    public GetOrderDetailsHandler(IOrderProvider provider)
    {
        _provider = provider;
    }

    public async Task<OrderHistoryDto> Handle(GetOrderDetailsQuery query, CancellationToken cancellationToken)
    {
        var order = await _provider.GetOrderById(query.IdOrder);
        if (order == null) throw new NotFoundException("Order not found");
        if (order.IdUser != query.IdUser) throw new ForbiddenException("You are not allowed to access this order");
        var dtos = order.Adapt<OrderHistoryDto>();
        return dtos;
    }

}