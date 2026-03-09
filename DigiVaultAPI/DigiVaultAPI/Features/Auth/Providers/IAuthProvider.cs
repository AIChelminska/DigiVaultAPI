using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Auth.Providers;

public interface IAuthProvider
{
    Task<User?> GetUserByLogin(string login);
    Task<User?> GetUserByEmail(string email);
    Task CreateUser(User user);
}
