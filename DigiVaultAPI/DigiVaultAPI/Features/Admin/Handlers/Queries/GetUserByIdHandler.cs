using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, AdminUserDetailDto>
{
    private readonly IAdminProvider _provider;

    public GetUserByIdHandler(IAdminProvider provider)
    {
        _provider = provider;
    }

    public async Task<AdminUserDetailDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _provider.GetUserById(query.IdUser);
        return user.Adapt<AdminUserDetailDto>();
    }
}