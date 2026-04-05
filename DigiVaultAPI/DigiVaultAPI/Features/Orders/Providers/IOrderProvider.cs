using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Orders.Providers;

public interface IOrderProvider
{
    Task<IEnumerable<Order>> GetOrders(int idUser);

    Task<Order?> GetOrderById(int idOrder);

}