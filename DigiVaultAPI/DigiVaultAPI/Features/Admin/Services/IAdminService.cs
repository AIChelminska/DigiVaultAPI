namespace DigiVaultAPI.Features.Admin.Services;

public interface IAdminService
{
    Task SetAsActiveUser(int idUser);
    Task SetAsNotActiveUser(int idUser);
    Task CreateCategory(string name);
    Task UpdateCategory(int idCategory, string name);
}