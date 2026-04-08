namespace DigiVaultAPI.Features.Admin.Messages.DTOs;

public class AdminOrdersDto
{
    public int IdOrder { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public int ItemsCount { get; set; }
    public DateTime CreatedAt { get; set; }
}