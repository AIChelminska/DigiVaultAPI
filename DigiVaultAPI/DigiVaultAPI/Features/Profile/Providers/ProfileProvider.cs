using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Profile.Providers;

public class ProfileProvider : IProfileProvider
{
    private readonly DigiVaultDbContext _context;

    public ProfileProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetProfile(int idUser)
    {
        var profile = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);
        return profile;
    }
}