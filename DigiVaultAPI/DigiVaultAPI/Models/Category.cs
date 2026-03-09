namespace DigiVaultAPI.Models;

public class Category
{
    public int IdCategory { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<Course> Courses { get; set; } = [];
}
