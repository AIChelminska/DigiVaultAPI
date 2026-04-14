using DigiVaultAPI.Data;
using DigiVaultAPI.Exceptions;
using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Courses.Services;

public class CourseService : ICourseService
{
    private readonly DigiVaultDbContext _context;

    public CourseService(DigiVaultDbContext context)
    {
        _context = context;
    }

    public void EnsureCourseExists(Course? course)
    {
        if (course == null)
            throw new NotFoundException("Kurs nie został znaleziony.");
    }

    public void EnsureCourseIsActive(Course course)
    {
        if (!course.IsActive)
            throw new NotFoundException("Kurs nie został znaleziony.");
    }

    public void EnsureCourseIsVisible(Course course)
    {
        if (!course.IsVisible)
            throw new NotFoundException("Kurs nie został znaleziony.");
    }

    public void EnsureIsAuthor(int courseOwnerId, int requestingUserId)
    {
        if (courseOwnerId != requestingUserId)
            throw new ForbiddenException("Nie masz uprawnień do tego kursu.");
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