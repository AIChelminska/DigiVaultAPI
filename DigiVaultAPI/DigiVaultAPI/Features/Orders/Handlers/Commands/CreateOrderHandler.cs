using DigiVaultAPI.Features.Orders.Messages.Commands;
using DigiVaultAPI.Features.Orders.Services;
using MediatR;

namespace DigiVaultAPI.Features.Orders.Handlers.Commands;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderService _orderService;

    public CreateOrderHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        return await _orderService.CreateOrder(command.IdUser);
    }
}