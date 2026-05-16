using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Cms.Providers;

public class CmsProvider : ICmsProvider
{
    private readonly DigiVaultDbContext _context;

    public CmsProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<List<CMSContent>> GetAll()
    {
        return await _context.CMSContents
            .OrderBy(c => c.Key)
            .ToListAsync();
    }

    public async Task<CMSContent?> GetByKey(string key)
    {
        return await _context.CMSContents
            .FirstOrDefaultAsync(c => c.Key == key);
    }
}
