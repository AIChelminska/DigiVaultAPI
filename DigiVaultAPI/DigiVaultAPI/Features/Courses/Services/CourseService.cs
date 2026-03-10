using DigiVaultAPI.Exceptions;
using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Courses.Services;

public class CourseService : ICourseService
{
    public void EnsureCourseExists(Course? course)
    {
        if (course == null)
            throw new NotFoundException("Kurs nie został znaleziony.");
    }

    public void EnsureCourseIsActive(Course course)
    {
        // Kurs usunięty przez admina traktowany jak nieistniejący
        if (!course.IsActive)
            throw new NotFoundException("Kurs nie został znaleziony.");
    }

    public void EnsureCourseIsVisible(Course course)
    {
        // Ukryty przez autora = 404 dla publicznych
        if (!course.IsVisible)
            throw new NotFoundException("Kurs nie został znaleziony.");
    }

    public void EnsureIsAuthor(int courseOwnerId, int requestingUserId)
    {
        if (courseOwnerId != requestingUserId)
            throw new ForbiddenException("Nie masz uprawnień do tego kursu.");
    }
}
