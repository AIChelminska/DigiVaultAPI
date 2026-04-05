using DigiVaultAPI.Data;
using DigiVaultAPI.Features.Review.Messages.DTOs;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace DigiVaultAPI.Features.Review.Providers;

public class ReviewProvider : IReviewProvider
{
    private readonly DigiVaultDbContext _context;

    public ReviewProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewById(int idCourse)
    {
        var reviews = await _context.Reviews
            .Include(r=>r.User)
            .Where(r=>r.IdCourse == idCourse)
            .ToListAsync();

        return reviews.Adapt<IEnumerable<ReviewDto>>();
    }
}
