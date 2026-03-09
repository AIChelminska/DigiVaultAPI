namespace DigiVaultAPI.Models;

public class CartItem
{
    public int IdCartItem { get; set; }
    public int IdUser { get; set; }     // klucz unikalny (IdUser, IdCourse)
    public int IdCourse { get; set; }   // klucz unikalny (IdUser, IdCourse)
    public DateTime AddedAt { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Course Course { get; set; } = null!;
}
