namespace DigiVaultAPI.Features.Orders.Services;

public interface IOrderService
{
    Task<int> CreateOrder(int idUser);
}