using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Courses.Providers;

public class CourseProvider : ICourseProvider
{
    private readonly DigiVaultDbContext _context;

    public CourseProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetCourses(string? search, int? idCategory, decimal? minPrice, decimal? maxPrice, string? sortBy, int page, int pageSize)
    {
        var query = _context.Courses
            .Include(c => c.User)
            .Include(c => c.Category)
            .Where(c => c.IsActive && c.IsVisible);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lower = search.ToLower();
            query = query.Where(c => c.Title.ToLower().Contains(lower) || c.Description.ToLower().Contains(lower));
        }

        if (idCategory.HasValue)
            query = query.Where(c => c.IdCategory == idCategory.Value);

        if (minPrice.HasValue)
            query = query.Where(c => c.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(c => c.Price <= maxPrice.Value);

        query = sortBy switch
        {
            "popular"    => query.OrderByDescending(c => c.SalesCount),
            "newest"     => query.OrderByDescending(c => c.CreatedAt),
            "top-rated"  => query.OrderByDescending(c => c.AverageRating),
            "price-asc"  => query.OrderBy(c => c.Price),
            "price-desc" => query.OrderByDescending(c => c.Price),
            _            => query.OrderByDescending(c => c.CreatedAt)
        };

        var courses = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return courses;
    }

    public async Task<int> GetCoursesCount(string? search, int? idCategory, decimal? minPrice, decimal? maxPrice)
    {
        var query = _context.Courses
            .Where(c => c.IsActive && c.IsVisible);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lower = search.ToLower();
            query = query.Where(c => c.Title.ToLower().Contains(lower) || c.Description.ToLower().Contains(lower));
        }

        if (idCategory.HasValue)
            query = query.Where(c => c.IdCategory == idCategory.Value);

        if (minPrice.HasValue)
            query = query.Where(c => c.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(c => c.Price <= maxPrice.Value);

        var count = await query.CountAsync();
        return count;
    }

    public async Task<Course?> GetCourseById(int idCourse)
    {
        var course = await _context.Courses
            .Include(c => c.User)
            .Include(c => c.Category)
            .FirstOrDefaultAsync(c => c.IdCourse == idCourse);

        return course;
    }

    public async Task<Course?> GetCourseByIdForEdit(int idCourse)
    {
        // Bez Include — zwraca też IsActive=false (usunięte przez admina)
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.IdCourse == idCourse);

        return course;
    }

    public async Task<List<Course>> GetPopularCourses(int limit)
    {
        var courses = await _context.Courses
            .Include(c => c.User)
            .Include(c => c.Category)
            .Where(c => c.IsActive && c.IsVisible)
            .OrderByDescending(c => c.SalesCount)
            .Take(limit)
            .ToListAsync();

        return courses;
    }

    public async Task<List<Course>> GetNewestCourses(int limit)
    {
        var courses = await _context.Courses
            .Include(c => c.User)
            .Include(c => c.Category)
            .Where(c => c.IsActive && c.IsVisible)
            .OrderByDescending(c => c.CreatedAt)
            .Take(limit)
            .ToListAsync();

        return courses;
    }

    public async Task<List<Course>> GetTopRatedCourses(int limit)
    {
        var courses = await _context.Courses
            .Include(c => c.User)
            .Include(c => c.Category)
            .Where(c => c.IsActive && c.IsVisible && c.RatingsCount > 0)
            .OrderByDescending(c => c.AverageRating)
            .Take(limit)
            .ToListAsync();

        return courses;
    }

    public async Task<List<Course>> GetSellerCourses(int idUser, int page, int pageSize)
    {
        // Wszystkie statusy — autor widzi swoje ukryte i usunięte kursy też
        var courses = await _context.Courses
            .Include(c => c.Category)
            .Where(c => c.IdUser == idUser)
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return courses;
    }

    public async Task<int> GetSellerCoursesCount(int idUser)
    {
        var count = await _context.Courses
            .CountAsync(c => c.IdUser == idUser);

        return count;
    }

    public async Task<int> CreateCourse(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return course.IdCourse;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
