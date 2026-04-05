using MediatR;

namespace DigiVaultAPI.Features.Orders.Messages.Commands;

public class CreateOrderCommand : IRequest<int>
{
    public int IdUser { get; set; }
}