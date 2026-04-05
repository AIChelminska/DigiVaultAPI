using MediatR;
using DigiVaultAPI.Features.Profile.Messages.DTOs;

namespace DigiVaultAPI.Features.Profile.Messages.Queries;

public class GetProfileQuery : IRequest<UserProfileDto>
{
    public int IdUser { get; set; }
}