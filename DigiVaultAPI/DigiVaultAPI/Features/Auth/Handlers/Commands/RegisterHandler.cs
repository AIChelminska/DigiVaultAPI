using DigiVaultAPI.Exceptions;
using DigiVaultAPI.Features.Auth.Messages.Commands;
using DigiVaultAPI.Features.Auth.Providers;
using DigiVaultAPI.Features.Auth.Services;
using DigiVaultAPI.Models;
using MediatR;

namespace DigiVaultAPI.Features.Auth.Handlers.Commands;

public class RegisterHandler : IRequestHandler<RegisterCommand>
{
    private readonly IAuthProvider _authProvider;
    private readonly IAuthService _authService;

    public RegisterHandler(IAuthProvider authProvider, IAuthService authService)
    {
        _authProvider = authProvider;
        _authService = authService;
    }

    public async Task Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // 1. Sprawdź unikalność loginu
        var existingLogin = await _authProvider.GetUserByLogin(command.Login);
        if (existingLogin != null) throw new ConflictException("Login jest już zajęty.");

        // 2. Sprawdź unikalność emaila
        var existingEmail = await _authProvider.GetUserByEmail(command.Email);
        if (existingEmail != null) throw new ConflictException("Email jest już zajęty.");

        // 3. Zahashuj hasło przez Service
        var hash = _authService.HashPassword(command.Password);

        // 4. Stwórz usera i zapisz przez Service
        var user = new User
        {
            Login = command.Login,
            Email = command.Email,
            PasswordHash = hash,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Role = UserRole.User,
            IsActive = true,
            Balance = 0
        };

        await _authService.CreateUser(user);
    }
}
