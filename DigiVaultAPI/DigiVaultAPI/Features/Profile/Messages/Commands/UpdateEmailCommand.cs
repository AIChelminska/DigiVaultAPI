using MediatR;

namespace DigiVaultAPI.Features.Profile.Messages.Commands;

public class UpdateEmailCommand : IRequest
{
    public int IdUser { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}