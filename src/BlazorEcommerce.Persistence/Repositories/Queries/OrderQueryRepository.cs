using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class OrderQueryRepository : QueryRepository<Order, int>, IOrderQueryRepository
{
    public OrderQueryRepository(PersistenceDataContext context) : base(context)
    {
    }

    public async Task<List<Order>> GetAllOrderByUserId(string userId)
    {
        return await context.Orders
                        .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                        .OrderByDescending(o => o.OrderDate)
                        .Where(o => o.UserId == userId)
                        .ToListAsync();
    }

    public async Task<Order> GetOrderDetails(string userId, int id)
    {
        var order = await context.Orders
           .Include(o => o.OrderItems)
           .ThenInclude(oi => oi.Product)
           .Include(o => o.OrderItems)
           .ThenInclude(oi => oi.ProductType)
           .Where(o => o.UserId == userId && o.Id == id)
           .OrderByDescending(o => o.OrderDate)
           .FirstOrDefaultAsync();

        return order;
    }
}
