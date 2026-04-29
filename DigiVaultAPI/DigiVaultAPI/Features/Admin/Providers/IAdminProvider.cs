using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.DTOs;
using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Admin.Providers;

public interface IAdminProvider
{
    Task<IEnumerable<User>> GetUsers();
    Task<IEnumerable<Order>> GetOrders(int page, int pageSize, string? search, DateTime? dateFrom, DateTime? dateTo);
    Task<int> GetOrdersCount(string? search, DateTime? dateFrom, DateTime? dateTo);
    Task<IEnumerable<Category>> GetCategories(int page, int pageSize, string? search);
    Task<int> GetCategoriesCount(string? search);
    Task<User> GetUserById(int idUser);
    Task<Course> GetCourseById(int idCourse);
}
