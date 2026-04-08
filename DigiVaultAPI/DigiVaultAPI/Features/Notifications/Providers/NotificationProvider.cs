using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Notifications.Providers;

public class NotificationProvider : INotificationProvider
{
    private readonly DigiVaultDbContext _context;

    public NotificationProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<List<Notification>> GetNotifications(int idUser)
    {
        return await _context.Notifications
            .Where(n => n.IdUser == idUser)
            .ToListAsync();
    }
}
