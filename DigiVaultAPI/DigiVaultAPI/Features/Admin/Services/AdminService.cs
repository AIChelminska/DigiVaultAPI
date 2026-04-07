using DigiVaultAPI.Data;
using DigiVaultAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Admin.Services;

public class AdminService : IAdminService
{
    private readonly DigiVaultDbContext _context;

    public AdminService(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task SetAsActiveUser(int idUser)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);
        if (user == null) throw new NotFoundException("User not found");
        user.IsActive = true;
        await _context.SaveChangesAsync();
    }

    public async Task SetAsNotActiveUser(int idUser)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);
        if (user == null) throw new NotFoundException("User not found");
        user.IsActive = false;
        await _context.SaveChangesAsync();
    }
}