using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Auth.Providers;

public class AuthProvider : IAuthProvider
{
    private readonly DigiVaultDbContext _context;

    public AuthProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByLogin(string login)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Login == login);

        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
