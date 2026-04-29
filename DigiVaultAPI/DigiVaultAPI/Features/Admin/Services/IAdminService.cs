using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Admin.Services;

public interface IAdminService
{
    Task SetAsActiveUser(int idUser);
    Task SetAsNotActiveUser(int idUser);
    Task CreateCategory(string name);
    Task UpdateCategory(int idCategory, string name);
    Task DeleteCategory(int idCategory);
    Task CreateUser(string login, string email, string password, string firstName, string lastName, UserRole role);
    Task UpdateUser(int idUser, UserRole role, int warningsCount, bool isActive);
    Task DeleteReview(int idReview);
    Task DeleteCourse(int idCourse);
}