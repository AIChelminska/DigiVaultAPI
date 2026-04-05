using DigiVaultAPI.Features.Profile.Messages.Commands;
using DigiVaultAPI.Features.Profile.Services;
using MediatR;

namespace DigiVaultAPI.Features.Profile.Handlers.Commands;

public class UpdateEmailHandler : IRequestHandler<UpdateEmailCommand>
{
    private readonly IProfileService _profileService;

    public UpdateEmailHandler(IProfileService profileService)
    {
        _profileService = profileService;
    }

    public async Task Handle(UpdateEmailCommand command, CancellationToken cancellationToken)
    {
        await _profileService.UpdateEmail(command.IdUser, command.Email, command.Password);
    }
}