namespace DigiVaultAPI.Features.Cart.Services;

public interface ICartService
{
    Task AddToCart(int idUser, int idCourse);
    Task RemoveFromCart(int idUser, int idCourse);
}