using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Profile.Providers;

public interface IProfileProvider
{
    Task<User?> GetProfile(int idUser);
}