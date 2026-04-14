using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Auth.Services;

public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
    string GenerateToken(User user);
    Task CreateUser(User user);
}
