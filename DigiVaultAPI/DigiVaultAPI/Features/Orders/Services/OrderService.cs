using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;
using DigiVaultAPI.Exceptions;

namespace DigiVaultAPI.Features.Orders.Services;

public class OrderService : IOrderService
{
    private readonly DigiVaultDbContext _context;

    public OrderService(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateOrder(int idUser)
    {
    await using var transaction = await _context.Database.BeginTransactionAsync();
    try
    {
        var cartItems = await _context.CartItems
        .Where(ci => ci.IdUser == idUser)
        .Include(ci => ci.Course)
        .ToListAsync();

        if(!cartItems.Any()) throw new ConflictException("Cart is empty");

        var order = new Order
        {
            IdUser = idUser,
            TotalPrice = cartItems.Sum(ci => ci.Course.Price),
        };

        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        var settings = await _context.PlatformSettings.FirstAsync();

        foreach(var cartItem in cartItems)
        {
            var orderItem = new OrderItem
            {
                IdOrder = order.IdOrder,
                IdCourse = cartItem.IdCourse,
                Price = cartItem.Course.Price,
                CommissionRate = settings.CommissionRate,
            };

            _context.OrderItems.Add(orderItem);
        
            _context.UserCourses.Add(new UserCourse
            {
                IdUser = idUser,
                IdCourse = cartItem.IdCourse,
            });

            _context.CartItems.Remove(cartItem);
            var wishlistItem = await _context.WishlistItems
                .FirstOrDefaultAsync(w => w.IdUser == idUser && w.IdCourse == cartItem.IdCourse);
            if (wishlistItem != null)
            {
                _context.WishlistItems.Remove(wishlistItem);
            }
        }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return order.IdOrder;
    }
    catch
    {
        await transaction.RollbackAsync();
            throw;
        }
    }
}

