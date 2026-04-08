using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Review.Providers;

public interface IReviewProvider
{
    Task<List<Review>> GetReviewById(int idCourse);
}
