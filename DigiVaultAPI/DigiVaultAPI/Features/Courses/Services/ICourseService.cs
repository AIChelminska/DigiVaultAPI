using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Courses.Services;

public interface ICourseService
{
    void EnsureCourseExists(Course? course);
    void EnsureCourseIsActive(Course course);
    void EnsureCourseIsVisible(Course course);
    void EnsureIsAuthor(int courseOwnerId, int requestingUserId);
}
