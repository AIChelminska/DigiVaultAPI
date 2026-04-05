namespace DigiVaultAPI.Features.Review.Services;

public interface IReviewService
{
    Task AddReview(int idUser, int idCourse, int rating, string? comment);
    Task DeleteReview(int idUser, int idCourse);
}