using DigiVaultAPI.Features.Review.Messages.DTOs;

namespace DigiVaultAPI.Features.Review.Providers;

public interface IReviewProvider
{
    Task<IEnumerable<ReviewDto>> GetReviewById(int idCourse);
}
