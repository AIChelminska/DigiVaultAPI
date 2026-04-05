using MediatR;
using DigiVaultAPI.Features.Orders.Messages.DTOs;

namespace DigiVaultAPI.Features.Orders.Messages.Queries;

public class GetOrderDetailsQuery : IRequest<OrderHistoryDto>
{
    public int IdOrder { get; set; }
    public int IdUser { get; set; }
}