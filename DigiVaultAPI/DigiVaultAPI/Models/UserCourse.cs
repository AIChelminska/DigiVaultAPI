namespace DigiVaultAPI.Models;

public class UserCourse
{
    public int IdUserCourse { get; set; }
    public int IdUser { get; set; }     // klucz unikalny (IdUser, IdCourse)
    public int IdCourse { get; set; }   // klucz unikalny (IdUser, IdCourse)
    public DateTime PurchasedAt { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Course Course { get; set; } = null!;
}
