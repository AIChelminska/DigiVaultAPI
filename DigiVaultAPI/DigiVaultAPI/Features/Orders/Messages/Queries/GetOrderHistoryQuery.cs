using DigiVaultAPI.Features.Orders.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Orders.Messages.Queries;

public class GetOrderHistoryQuery : IRequest<List<OrderHistoryDto>>
{
    public int IdUser { get; set; }
}