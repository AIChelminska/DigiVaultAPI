using DigiVaultAPI.Data;
using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace DigiVaultAPI.Features.Admin.Providers;

public class UsersProvider : IUsersProvider
{
    private readonly DigiVaultDbContext _context;

    public UsersProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AdminUserDto>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users.Adapt<IEnumerable<AdminUserDto>>();
    }
}