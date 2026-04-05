using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Wishlist.Services;

public class WishlistService : IWishlistService
{
    private readonly DigiVaultDbContext _context;
    
    public WishlistService(DigiVaultDbContext context)
    {
        _context = context;
    }
    
    public async Task AddToWishlist(int idUser, int idCourse)
    {
        var item = await _context.WishlistItems
            .FirstOrDefaultAsync(w => w.IdUser == idUser && w.IdCourse == idCourse);
        if (item == null)
        {
            _context.WishlistItems.Add(new WishlistItem { IdUser = idUser, IdCourse = idCourse });
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task RemoveFromWishlist(int idUser, int idCourse)
    {
        var item = await _context.WishlistItems
            .FirstOrDefaultAsync(w => w.IdUser == idUser && w.IdCourse == idCourse);
        if (item != null)
        {
            _context.WishlistItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}