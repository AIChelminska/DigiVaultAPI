namespace DigiVaultAPI.Features.Admin.Messages.DTOs;

public class AdminUserDetailDto
{
    public int IdUser { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; }
    public decimal Balance { get; set; } = 0;
    public decimal TotalWithdrawn { get; set; } = 0;
    public int WarningsCount { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public List<AdminUserCourseDto> CreatedCourses { get; set; } = [];
    public List<AdminUserCourseDto> PurchasedCourses { get; set; } = [];
}

public class AdminUserCourseDto
{
    public int IdCourse { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}