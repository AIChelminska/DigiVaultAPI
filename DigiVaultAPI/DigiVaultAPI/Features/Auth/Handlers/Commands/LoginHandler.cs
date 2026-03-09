using DigiVaultAPI.Exceptions;
using DigiVaultAPI.Features.Auth.Messages.Commands;
using DigiVaultAPI.Features.Auth.Providers;
using DigiVaultAPI.Features.Auth.Services;
using MediatR;

namespace DigiVaultAPI.Features.Auth.Handlers.Commands;

public class LoginHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IAuthProvider _authProvider;
    private readonly IAuthService _authService;

    public LoginHandler(IAuthProvider authProvider, IAuthService authService)
    {
        _authProvider = authProvider;
        _authService = authService;
    }

    public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        // 1. Pobierz usera z bazy przez Provider
        var user = await _authProvider.GetUserByLogin(command.Login);
        if (user == null) throw new UnauthorizedException("Nieprawidłowy login lub hasło.");
        if (!user.IsActive) throw new ForbiddenException("Konto jest nieaktywne.");

        // 2. Zweryfikuj hasło przez Service
        var valid = _authService.VerifyPassword(command.Password, user.PasswordHash);
        if (!valid) throw new UnauthorizedException("Nieprawidłowy login lub hasło.");

        // 3. Wygeneruj token przez Service
        var token = _authService.GenerateToken(user);

        // 4. Zwróć token — frontend dekoduje claims z JWT
        return token;
    }
}
