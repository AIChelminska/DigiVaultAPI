namespace DigiVaultAPI.Features.Wishlist.Services;

public interface IWishlistService
{
    Task AddToWishlist(int idUser, int idCourse);
    Task RemoveFromWishlist(int idUser, int idCourse);
}