namespace DigiVaultAPI.Models;

public class Course
{
    public int IdCourse { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }                        // opcjonalne
    public int IdCategory { get; set; }
    public int IdUser { get; set; }                              // autor
    public int SalesCount { get; set; } = 0;
    public int RatingsCount { get; set; } = 0;
    public decimal AverageRating { get; set; } = 0;
    public bool IsActive { get; set; } = true;                  // false = trwale usunięty
    public bool IsVisible { get; set; } = true;                 // false = ukryty przez autora
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;              // autor
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
    public virtual ICollection<UserCourse> UserCourses { get; set; } = [];
    public virtual ICollection<Review> Reviews { get; set; } = [];
    public virtual ICollection<CartItem> CartItems { get; set; } = [];
    public virtual ICollection<WishlistItem> WishlistItems { get; set; } = [];
    public virtual ICollection<CourseReport> CourseReports { get; set; } = [];
}
