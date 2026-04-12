using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<AdminUserDto>>
{
    private readonly IAdminProvider _provider;

    public GetUsersHandler(IAdminProvider provider)
    {
        _provider = provider;
    }

    public async Task<IEnumerable<AdminUserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _provider.GetUsers();
        return users.Adapt<IEnumerable<AdminUserDto>>();
    }
}
