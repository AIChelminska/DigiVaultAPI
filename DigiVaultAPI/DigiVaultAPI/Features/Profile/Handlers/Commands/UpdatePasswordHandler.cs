using DigiVaultAPI.Features.Profile.Messages.Commands;
using DigiVaultAPI.Features.Profile.Services;
using MediatR;

namespace DigiVaultAPI.Features.Profile.Handlers.Commands;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordCommand>
{
    private readonly IProfileService _profileService;

    public UpdatePasswordHandler(IProfileService profileService)
    {
        _profileService = profileService;
    }

    public async Task Handle(UpdatePasswordCommand command, CancellationToken cancellationToken)
    {
        await _profileService.UpdatePassword(command.IdUser, command.Password, command.NewPassword, command.NewPasswordConfirmation);
    }
}