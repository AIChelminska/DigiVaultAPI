using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Providers;
using DigiVaultAPI.Features.Admin.Messages.DTOs;
using MediatR;
using Mapster;

namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetOrderByIdAdminHandler : IRequestHandler<GetOrderByIdAdminQuery, AdminOrderDetailsDto>
{
    private readonly IAdminProvider _provider;

    public GetOrderByIdAdminHandler(IAdminProvider provider)
    {
        _provider = provider;
    }

    public async Task<AdminOrderDetailsDto> Handle(GetOrderByIdAdminQuery request, CancellationToken cancellationToken)
    {
        var order = await _provider.GetOrderByIdAdmin(request.IdOrder);
        return order.Adapt<AdminOrderDetailsDto>();
    }
}