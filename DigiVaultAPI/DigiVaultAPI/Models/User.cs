namespace DigiVaultAPI.Models;

public class User
{
    public int IdUser { get; set; }
    public string Login { get; set; } = string.Empty;           // UNIQUE
    public string Email { get; set; } = string.Empty;           // UNIQUE
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    public decimal Balance { get; set; } = 0;                   // saldo do wypłaty
    public decimal TotalWithdrawn { get; set; } = 0;            // łącznie wypłacone
    public int WarningsCount { get; set; } = 0;
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<Course> Courses { get; set; } = [];         // kursy jako autor
    public virtual ICollection<Order> Orders { get; set; } = [];
    public virtual ICollection<CartItem> CartItems { get; set; } = [];
    public virtual ICollection<WishlistItem> WishlistItems { get; set; } = [];
    public virtual ICollection<UserCourse> UserCourses { get; set; } = []; // zakupione kursy
    public virtual ICollection<Review> Reviews { get; set; } = [];
    public virtual ICollection<Notification> Notifications { get; set; } = [];
    public virtual ICollection<CourseReport> CourseReports { get; set; } = [];
}
