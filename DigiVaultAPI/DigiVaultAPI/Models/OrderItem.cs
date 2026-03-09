namespace DigiVaultAPI.Models;

public class OrderItem
{
    public int IdOrderItem { get; set; }
    public int IdOrder { get; set; }
    public int IdCourse { get; set; }
    public decimal Price { get; set; }           // snapshot ceny z momentu zakupu
    public decimal CommissionRate { get; set; }  // snapshot prowizji z momentu zakupu

    // Navigation properties
    public virtual Order Order { get; set; } = null!;
    public virtual Course Course { get; set; } = null!;
}
