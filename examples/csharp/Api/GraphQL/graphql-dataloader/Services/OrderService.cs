using HttpApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HttpApi.Services;

public interface IOrderService
{
    Task<Order> Get(int id);
}

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> Get(int id)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }
}