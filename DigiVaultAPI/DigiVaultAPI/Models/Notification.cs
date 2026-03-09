namespace DigiVaultAPI.Models;

public class Notification
{
    public int IdNotification { get; set; }
    public int IdUser { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
}
