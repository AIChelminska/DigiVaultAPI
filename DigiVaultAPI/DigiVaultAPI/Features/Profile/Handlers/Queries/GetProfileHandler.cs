using DigiVaultAPI.Exceptions;
using DigiVaultAPI.Features.Profile.Messages.DTOs;
using DigiVaultAPI.Features.Profile.Messages.Queries;
using DigiVaultAPI.Features.Profile.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Profile.Handlers.Queries;

public class GetProfileHandler : IRequestHandler<GetProfileQuery, UserProfileDto>
{
    private readonly IProfileProvider _provider;

    public GetProfileHandler(IProfileProvider provider)
    {
        _provider = provider;
    }

    public async Task<UserProfileDto> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    {
        var profile = await _provider.GetProfile(query.IdUser);
        if (profile == null) throw new NotFoundException("Profile not found");
        return profile.Adapt<UserProfileDto>();
    }
}