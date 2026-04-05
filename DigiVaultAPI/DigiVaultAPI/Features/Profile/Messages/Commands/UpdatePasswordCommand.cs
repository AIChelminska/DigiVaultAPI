using MediatR;

namespace DigiVaultAPI.Features.Profile.Messages.Commands;

public class UpdatePasswordCommand : IRequest
{
    public int IdUser { get; set; }
    public string Password { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string NewPasswordConfirmation { get; set; } = string.Empty;
}