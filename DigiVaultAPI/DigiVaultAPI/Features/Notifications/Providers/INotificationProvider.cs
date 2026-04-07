using DigiVaultAPI.Features.Notifications.Messages.DTOs;

namespace DigiVaultAPI.Features.Notifications.Providers;

public interface INotificationProvider
{
    Task<IEnumerable<NotificationDto>> GetNotifications(int idUser);
}