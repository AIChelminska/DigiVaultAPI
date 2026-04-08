using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Notifications.Providers;

public interface INotificationProvider
{
    Task<List<Notification>> GetNotifications(int idUser);
}
