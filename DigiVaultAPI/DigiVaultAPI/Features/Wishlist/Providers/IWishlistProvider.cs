using DigiVaultAPI.Models;
namespace DigiVaultAPI.Features.Wishlist.Providers;

public interface IWishlistProvider
{
    Task<IEnumerable<Course>> GetWishlistItemsAsync(int idUser);
}