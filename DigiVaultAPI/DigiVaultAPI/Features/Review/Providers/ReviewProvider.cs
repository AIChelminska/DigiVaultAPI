using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Review.Providers;

public class ReviewProvider : IReviewProvider
{
    private readonly DigiVaultDbContext _context;

    public ReviewProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<List<Review>> GetReviewById(int idCourse)
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Where(r => r.IdCourse == idCourse)
            .ToListAsync();
    }
}
