using MediatR;

namespace DigiVaultAPI.Features.Auth.Messages.Commands;

public class LoginCommand : IRequest<string>
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
