using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Cart.Providers;

public interface ICardProvider
{
    Task<>> GetCartItems(int idUser);
}