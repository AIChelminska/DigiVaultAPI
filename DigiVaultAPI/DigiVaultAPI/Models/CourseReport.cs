namespace DigiVaultAPI.Models;

public class CourseReport
{
    public int IdCourseReport { get; set; }
    public int IdCourse { get; set; }   // klucz unikalny (IdUser, IdCourse)
    public int IdUser { get; set; }     // klucz unikalny (IdUser, IdCourse)
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsResolved { get; set; } = false;

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Course Course { get; set; } = null!;
}
