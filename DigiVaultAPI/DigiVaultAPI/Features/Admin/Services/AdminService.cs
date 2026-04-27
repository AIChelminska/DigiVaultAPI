using DigiVaultAPI.Data;
using DigiVaultAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using DigiVaultAPI.Models;
using BCrypt.Net;

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

    public async Task CreateCategory(string name)
    {
        var exists = await _context.Categories.AnyAsync(c => c.Name == name);
        if (exists) throw new ConflictException("Category already exists");

        var category = new Category
        {
            Name = name,
            IsActive = true
        };
        
        
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategory(int idCategory, string name)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.IdCategory == idCategory);
        if (category == null) throw new NotFoundException("Category not found");
        var exists = await _context.Categories.AnyAsync(c => c.Name == name && c.IdCategory != idCategory);
        if (exists) throw new ConflictException("Category with this name already exists");
        category.Name = name;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategory(int idCategory)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.IdCategory == idCategory);
        if (category == null) throw new NotFoundException("Category not found");
        var hasCourses = await _context.Courses.AnyAsync(c => c.IdCategory == idCategory);
        if (hasCourses) throw new ConflictException("Cannot delete category with existing courses");
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task CreateUser(string login, string email, string password, string firstName, string lastName, UserRole role)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == login || u.Email == email);
        if (existingUser != null) throw new ConflictException("User with this login or email already exists");
        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User
        {
            Login = login,
            Email = email,
            PasswordHash = hash,
            FirstName = firstName,
            LastName = lastName,
            Role = role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(int idUser, UserRole role, int warningsCount, bool isActive)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);
        if (user == null) throw new NotFoundException("User not found");
        user.Role = role;
        user.WarningsCount = warningsCount;
        user.IsActive = isActive;
        await _context.SaveChangesAsync();
    }
}