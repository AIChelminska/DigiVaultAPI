using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Features.Admin.Providers;

public class UsersProvider : IUsersProvider
{
    private readonly DigiVaultDbContext _context;

    public UsersProvider(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<List<Order>> GetOrders(int page, int pageSize, string? search, DateTime? dateFrom, DateTime? dateTo)
    {
        var query = _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems);
            


        if (!string.IsNullOrWhiteSpace(search))
        {
            var lower = search.ToLower();
            query = query.Where(o => o.User.FirstName.ToLower().Contains(lower) || o.User.LastName.ToLower().Contains(lower) || o.User.Email.ToLower().Contains(lower));
        }

        if (dateFrom.HasValue)
        {
            query = query.Where(o => o.CreatedAt >= dateFrom.Value);
        }

        if (dateTo.HasValue)
        {
            query = query.Where(o => o.CreatedAt <= dateTo.Value);
        }

        var orders = await query
            .OrderByDescending(o => o.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return orders;
    }

    public async Task<int> GetOrdersCount(string? search, DateTime? dateFrom, DateTime? dateTo)
    {
        var query = _context.Orders
            .Include(o => o.User);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lower = search.ToLower();
            query = query.Where(o => o.User.FirstName.ToLower().Contains(lower) || o.User.LastName.ToLower().Contains(lower) || o.User.Email.ToLower().Contains(lower));
        }

        if (dateFrom.HasValue)
        {
            query = query.Where(o => o.CreatedAt >= dateFrom.Value);
        }

        if (dateTo.HasValue)
        {
            query = query.Where(o => o.CreatedAt <= dateTo.Value);
        }

        var orders =  await query.CountAsync();
        return orders;
    }
}
