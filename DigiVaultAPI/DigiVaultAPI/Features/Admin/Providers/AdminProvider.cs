using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;
using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Exceptions;
using Mapster;

namespace DigiVaultAPI.Features.Admin.Providers;

public class AdminProvider : IAdminProvider
{
    private readonly DigiVaultDbContext _context;

    public AdminProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrders(int page, int pageSize, string? search, DateTime? dateFrom, DateTime? dateTo)
    {
        var query = _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lower = search.ToLower();
            query = query.Where(o => o.User.FirstName.ToLower().Contains(lower) || o.User.LastName.ToLower().Contains(lower) || o.User.Email.ToLower().Contains(lower));
        }

        if (dateFrom.HasValue)
            query = query.Where(o => o.CreatedAt >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(o => o.CreatedAt <= dateTo.Value);

        return await query
            .OrderByDescending(o => o.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetOrdersCount(string? search, DateTime? dateFrom, DateTime? dateTo)
    {
        var query = _context.Orders
            .Include(o => o.User)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lower = search.ToLower();
            query = query.Where(o => o.User.FirstName.ToLower().Contains(lower) || o.User.LastName.ToLower().Contains(lower) || o.User.Email.ToLower().Contains(lower));
        }

        if (dateFrom.HasValue)
            query = query.Where(o => o.CreatedAt >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(o => o.CreatedAt <= dateTo.Value);

        return await query.CountAsync();
    }

    public async Task<IEnumerable<Category>> GetCategories(int page, int pageSize, string? search)
    {
        var query = _context.Categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lower = search.ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(lower));
        }

        return await query
            .OrderByDescending(c => c.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();   
        
    }

    public async Task<int> GetCategoriesCount(string? search)
    {
        var query = _context.Categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lower = search.ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(lower));
        }

        return await query.CountAsync();
    }

    public async Task<User> GetUserById(int idUser)
    {
        return await _context.Users
            .Include(u => u.Courses)
            .Include(u => u.UserCourses)
                .ThenInclude(uc => uc.Course)
            .FirstOrDefaultAsync(u => u.IdUser == idUser) ?? throw new NotFoundException("User not found");
    }

    public async Task<Course> GetCourseById(int idCourse)
    {
        var course = await _context.Courses
            .Include(c => c.User)
            .Include(c => c.Category)
            .Include(c => c.CourseReports)
            .FirstOrDefaultAsync(c => c.IdCourse == idCourse);

        return course ?? throw new NotFoundException("Course not found");
    }

    public async Task<Order> GetOrderByIdAdmin(int idOrder)
    {
        var order = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Course)
            .FirstOrDefaultAsync(o => o.IdOrder == idOrder);

        return order ?? throw new NotFoundException("Order not found");
    }

    public async Task<IEnumerable<Notification>> GetNotificationsAdmin(int page, int pageSize, string? search, bool isRead)
    {
        var query = _context.Notifications
            .Include(n => n.User)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lower = search.ToLower();
            query = query.Where(n => n.Title.ToLower().Contains(lower) || n.Message.ToLower().Contains(lower));
        }

        if (isRead)
            query = query.Where(n => n.IsRead);

        return await query.OrderByDescending(n => n.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetNotificationsAdminCount(string? search, bool isRead)
    {
        var query = _context.Notifications.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lower = search.ToLower();
            query = query.Where(n => n.Title.ToLower().Contains(lower) || n.Message.ToLower().Contains(lower));
        }

        if (isRead)
            query = query.Where(n => n.IsRead);

        return await query.CountAsync();
    }

    public async Task<AdminSettingsDto> GetSettings()
    {
        var settings = await _context.PlatformSettings.FirstOrDefaultAsync();
        if (settings == null) throw new NotFoundException("Settings not found");
        return settings.Adapt<AdminSettingsDto>();
    }

    public async Task<IEnumerable<CourseReport>> GetReportsAdmin(bool? isResolved, int page, int pageSize)
    {
        var query = _context.CourseReports
            .Include(r => r.User)
            .Include(r => r.Course)
            .AsQueryable();
        if (isResolved.HasValue)
            query = query.Where(r => r.IsResolved == isResolved.Value);
        return await query.OrderByDescending(r => r.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetReportsAdminCount(bool? isResolved)
    {
        var query = _context.CourseReports.AsQueryable();
        if (isResolved.HasValue)
            query = query.Where(r => r.IsResolved == isResolved.Value);
        return await query.CountAsync();
    }
}