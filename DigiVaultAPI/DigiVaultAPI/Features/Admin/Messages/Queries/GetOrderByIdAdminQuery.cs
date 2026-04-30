using DigiVaultAPI.Features.Admin.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Queries;

public class GetOrderByIdAdminQuery : IRequest<AdminOrderDetailsDto>
{
    public int IdOrder { get; set; }
}