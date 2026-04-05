namespace DigiVaultAPI.Features.Review.Messages.DTOs;

public class ReviewDto
{
    public int IdReview { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
