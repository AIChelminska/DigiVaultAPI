namespace DigiVaultAPI.Features.Orders.Messages.DTOs;

public class OrderItemDto
{
    public int IdCourse { get; set; }
    public decimal Price { get; set; }  
    public string Title { get; set; } = string.Empty;
}
    
