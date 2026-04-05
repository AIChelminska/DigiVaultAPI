using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Cart.Providers;

public interface ICartProvider
{
    Task<IEnumerable<Course>> GetCartItems(int idUser);
}