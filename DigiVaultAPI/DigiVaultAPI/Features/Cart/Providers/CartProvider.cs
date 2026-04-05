using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Cart.Providers;

public class CartProvider : ICartProvider
{
    private readonly DigiVaultDbContext _context;
    
    public CartProvider(DigiVaultDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Course>> GetCartItems(int idUser)
    {
        return await _context.CartItems
            .Where(c => c.IdUser == idUser)
            .Select(c => c.Course!)
            .ToListAsync();
    }
}