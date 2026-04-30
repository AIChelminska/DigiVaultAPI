using DigiVaultAPI.Features.Orders.Messages.DTOs;

namespace DigiVaultAPI.Features.Admin.Messages.DTOs;

public class AdminOrderDetailsDto : OrderHistoryDto
{
    public int IdUser { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}