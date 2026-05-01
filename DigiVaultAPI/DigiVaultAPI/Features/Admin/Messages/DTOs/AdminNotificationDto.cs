namespace DigiVaultAPI.Features.Admin.Messages.DTOs;

public class AdminNotificationDto
{
    public int IdNotification { get; set; }
    public int IdUser { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}