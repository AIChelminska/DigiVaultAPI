namespace DigiVaultAPI.Features.Courses.Messages.DTOs;

public class SellerCourseDto
{
    public int IdCourse { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int SalesCount { get; set; }
    public double AverageRating { get; set; }
    public int RatingsCount { get; set; }
    public bool IsActive { get; set; }
    public bool IsVisible { get; set; }
    public DateTime CreatedAt { get; set; }
}
