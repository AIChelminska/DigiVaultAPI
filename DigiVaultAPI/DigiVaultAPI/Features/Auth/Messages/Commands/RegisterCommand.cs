using MediatR;

namespace DigiVaultAPI.Features.Auth.Messages.Commands;

// IRequest bez typu — Register nic nie zwraca, tylko tworzy konto
public class RegisterCommand : IRequest
{
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
