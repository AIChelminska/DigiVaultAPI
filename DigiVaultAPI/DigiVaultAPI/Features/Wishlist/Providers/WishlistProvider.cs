using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Wishlist.Providers;

public class WishlistProvider : IWishlistProvider
{
    private readonly DigiVaultDbContext _context;
    
    public WishlistProvider(DigiVaultDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Course>> GetWishlistItemsAsync(int idUser)
    {
        return await _context.WishlistItems
            .Where(w => w.IdUser == idUser)
            .Include(w => w.Course)
            .ThenInclude(c => c.User)
            .Include(w => w.Course)
            .ThenInclude(c => c.Category)
            .Select(w => w.Course)
            .ToListAsync();
    }
}