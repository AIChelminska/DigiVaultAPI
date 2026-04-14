using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Orders.Services;

public interface IOrderService
{
    Task<int> CreateOrder(int idUser);
    void EnsureOrderExists(Order? order);
    void EnsureOrderBelongsToUser(Order order, int idUser);
}