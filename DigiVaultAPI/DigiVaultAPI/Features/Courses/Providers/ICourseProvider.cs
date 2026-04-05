using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Courses.Providers;

public interface ICourseProvider
{
    // Queries
    Task<List<Course>> GetCourses(string? search, int? idCategory, decimal? minPrice, decimal? maxPrice, string? sortBy, int page, int pageSize);
    Task<int> GetCoursesCount(string? search, int? idCategory, decimal? minPrice, decimal? maxPrice);
    Task<Course?> GetCourseById(int idCourse);
    Task<Course?> GetCourseByIdForEdit(int idCourse);
    Task<List<Course>> GetPopularCourses(int limit);
    Task<List<Course>> GetNewestCourses(int limit);
    Task<List<Course>> GetTopRatedCourses(int limit);
    Task<List<Course>> GetSellerCourses(int idUser, int page, int pageSize);
    Task<int> GetSellerCoursesCount(int idUser);
    Task<List<Course>> GetUserCourses(int idUser);

    // Commands
    Task<int> CreateCourse(Course course);
    Task SaveChanges();
}
