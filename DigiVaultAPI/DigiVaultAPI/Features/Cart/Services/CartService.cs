using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Cart.Services;

public class CartService : ICartService
{
    private readonly DigiVaultDbContext _context;
    
    public CartService(DigiVaultDbContext context)
    {
        _context = context;
    }
    
    public async Task AddToCart(int idUser, int idCourse)
    {
        var item = await _context.CartItems
            .FirstOrDefaultAsync(c => c.IdUser == idUser && c.IdCourse == idCourse);
        if (item == null)
        {
            _context.CartItems.Add(new CartItem { IdUser = idUser, IdCourse = idCourse });
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task RemoveFromCart(int idUser, int idCourse)
    {
        var item = await _context.CartItems
            .FirstOrDefaultAsync(c => c.IdUser == idUser && c.IdCourse == idCourse);
        if (item != null)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}