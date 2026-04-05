namespace DigiVaultAPI.Features.Orders.Messages.DTOs;

public class OrderHistoryDto
{
    public int IdOrder { get; set; }
    public decimal TotalPrice { get; set; }
    public int ItemsCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = [];
}