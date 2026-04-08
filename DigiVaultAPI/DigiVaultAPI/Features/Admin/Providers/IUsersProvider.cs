using DigiVaultAPI.Models;
using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.DTOs;

namespace DigiVaultAPI.Features.Admin.Providers;

public interface IUsersProvider
{
    Task<List<User>> GetUsers();
    Task<List<Order>> GetOrders(int page, int pageSize);
    Task<int> GetOrdersCount();
}
