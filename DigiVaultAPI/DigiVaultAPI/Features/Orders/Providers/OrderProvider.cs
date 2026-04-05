using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Orders.Providers;

public class OrderProvider : IOrderProvider
{
    private readonly DigiVaultDbContext _context;

    public OrderProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetOrders(int idUser)
    {
        return await _context.Orders
            .Where(o => o.IdUser == idUser)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Course)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderById(int idOrder)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Course)
            .FirstOrDefaultAsync(o => o.IdOrder == idOrder);
    }
}