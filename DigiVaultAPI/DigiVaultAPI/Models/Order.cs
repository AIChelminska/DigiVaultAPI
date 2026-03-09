namespace DigiVaultAPI.Models;

public class Order
{
    public int IdOrder { get; set; }
    public int IdUser { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
}
