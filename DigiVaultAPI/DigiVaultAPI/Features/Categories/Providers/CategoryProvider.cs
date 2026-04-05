using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Categories.Providers;

public class CategoryProvider : ICategoryProvider
{
    private readonly DigiVaultDbContext _context;
    
    public CategoryProvider(DigiVaultDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Category>> GetCategories()
    {
        var query = _context.Categories
            .Where(c => c.IsActive)
            .ToListAsync();
        
        return await query;
    }
}