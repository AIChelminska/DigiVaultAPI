using DigiVaultAPI.Features.Profile.Messages.Commands;
using DigiVaultAPI.Features.Profile.Services;
using MediatR;

namespace DigiVaultAPI.Features.Profile.Handlers.Commands;

public class UpdateNameHandler : IRequestHandler<UpdateNameCommand>
{
    private readonly IProfileService _profileService;

    public UpdateNameHandler(IProfileService profileService)
    {
        _profileService = profileService;
    }

    public async Task Handle(UpdateNameCommand command, CancellationToken cancellationToken)
    {
        await _profileService.UpdateName(command.IdUser, command.FirstName, command.LastName);
    }
}