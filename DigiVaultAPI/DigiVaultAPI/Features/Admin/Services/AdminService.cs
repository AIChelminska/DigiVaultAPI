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

    public async Task DeleteReview(int idReview)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.IdReview == idReview);
        if (review == null) throw new NotFoundException("Review not found");
        _context.Reviews.Remove(review);
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.IdCourse == review.IdCourse);
        if (course != null)
        {
            if (course.RatingsCount > 1)
            {
                course.AverageRating = (course.AverageRating * course.RatingsCount - review.Rating) / (course.RatingsCount - 1);
            }
            else
            {
                course.AverageRating = 0;
            }
            course.RatingsCount--;
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCourse(int idCourse)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.IdCourse == idCourse);
        if (course == null) throw new NotFoundException("Course not found");
        course.IsActive = false;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrder(int idOrder)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
        var order = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.IdOrder == idOrder);
        if (order == null) throw new NotFoundException("Order not found");

        order.User.Balance += order.TotalPrice;

        var courseIds = order.OrderItems.Select(oi => oi.IdCourse).ToList();
        var userCourses = await _context.UserCourses
            .Where(uc => uc.IdUser == order.IdUser && courseIds.Contains(uc.IdCourse))
            .ToListAsync();
        _context.UserCourses.RemoveRange(userCourses);
        
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task RemoveCourseFromOrder(int idOrder, int idCourse)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var orderItem = await _context.OrderItems
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.User)
                .FirstOrDefaultAsync(oi => oi.IdOrder == idOrder && oi.IdCourse == idCourse);
            if (orderItem == null) throw new NotFoundException("Course not found in this order");

            orderItem.Order.User.Balance += orderItem.Price;

            var userCourse = await _context.UserCourses
                .FirstOrDefaultAsync(uc => uc.IdUser == orderItem.Order.IdUser && uc.IdCourse == idCourse);
            if (userCourse != null)
                _context.UserCourses.Remove(userCourse);

            orderItem.Order.TotalPrice -= orderItem.Price;
            _context.OrderItems.Remove(orderItem);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateSettings(decimal commissionRate)
    {
        var settings = await _context.PlatformSettings.FirstOrDefaultAsync();
        if (settings == null) throw new NotFoundException("Settings not found");
        settings.CommissionRate = commissionRate;
        await _context.SaveChangesAsync();
    }
}