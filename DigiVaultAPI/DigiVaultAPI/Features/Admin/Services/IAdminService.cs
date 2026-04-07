namespace DigiVaultAPI.Features.Admin.Services;

public interface IAdminService
{
    Task SetAsActiveUser(int idUser);
    Task SetAsNotActiveUser(int idUser);
}