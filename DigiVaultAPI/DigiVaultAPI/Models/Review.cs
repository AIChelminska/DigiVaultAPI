namespace DigiVaultAPI.Models;

public class Review
{
    public int IdReview { get; set; }
    public int IdCourse { get; set; }   // klucz unikalny (IdUser, IdCourse)
    public int IdUser { get; set; }     // klucz unikalny (IdUser, IdCourse)
    public int Rating { get; set; }     // 1-5
    public string? Comment { get; set; }    // opcjonalny
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Course Course { get; set; } = null!;
}
