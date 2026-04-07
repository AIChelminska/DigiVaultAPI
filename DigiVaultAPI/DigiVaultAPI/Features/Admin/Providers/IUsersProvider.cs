using DigiVaultAPI.Features.Admin.Messages.DTOs;

namespace DigiVaultAPI.Features.Admin.Providers;

public interface IUsersProvider
{
    Task<IEnumerable<AdminUserDto>> GetUsers();
}