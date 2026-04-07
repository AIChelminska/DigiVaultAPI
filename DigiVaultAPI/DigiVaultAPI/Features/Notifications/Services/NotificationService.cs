using DigiVaultAPI.Data;
using DigiVaultAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Notifications.Services;

public class NotificationService : INotificationService
{
    private readonly DigiVaultDbContext _context;

    public NotificationService(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task SetAsReadedNotification(int idNotification, int idUser)
    {
        var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.IdNotification == idNotification && n.IdUser == idUser);
        if (notification == null) throw new NotFoundException("Notification not found");
        notification.IsRead = true;
        await _context.SaveChangesAsync();
    }
}