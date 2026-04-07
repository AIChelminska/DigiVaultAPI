namespace DigiVaultAPI.Features.Notifications.Messages.DTOs;

public class NotificationDto
{
    public int IdNotification { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; }
}