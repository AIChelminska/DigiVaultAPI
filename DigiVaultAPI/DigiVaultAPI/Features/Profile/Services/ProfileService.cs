using DigiVaultAPI.Data;
using DigiVaultAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Profile.Services;

public class ProfileService : IProfileService
{
    private readonly DigiVaultDbContext _context;

    public ProfileService(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task UpdateName(int idUser, string firstName, string lastName)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);
        if (user == null) throw new NotFoundException("User not found");
        user.FirstName = firstName;
        user.LastName = lastName;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEmail(int idUser, string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);
        if (user == null) throw new NotFoundException("User not found");
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) throw new UnauthorizedException("Invalid password");
        user.Email = email;
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePassword(int idUser, string password, string newPassword, string newPasswordConfirmation)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);
        if (user == null) throw new NotFoundException("User not found");
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) throw new UnauthorizedException("Invalid password");
        if (newPassword != newPasswordConfirmation) throw new ConflictException("New passwords do not match");
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _context.SaveChangesAsync();
    }
}