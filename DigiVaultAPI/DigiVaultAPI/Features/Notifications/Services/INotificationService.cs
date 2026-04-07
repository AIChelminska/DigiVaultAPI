namespace DigiVaultAPI.Features.Notifications.Services;

public interface INotificationService
{
    Task SetAsReadedNotification(int idNotification, int idUser);
}