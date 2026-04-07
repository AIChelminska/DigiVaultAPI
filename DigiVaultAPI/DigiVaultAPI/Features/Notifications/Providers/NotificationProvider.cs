using DigiVaultAPI.Data;
using DigiVaultAPI.Features.Notifications.Messages.DTOs;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace DigiVaultAPI.Features.Notifications.Providers;

public class NotificationProvider : INotificationProvider
{
    private readonly DigiVaultDbContext _context;

    public NotificationProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NotificationDto>> GetNotifications(int idUser)
    {
        var notifications = await _context.Notifications
            .Where(n => n.IdUser == idUser)
            .ToListAsync();
        return notifications.Adapt<IEnumerable<NotificationDto>>();
    }
}