namespace BlazorEcommerce.Application.Repositories.Queries;

public interface IOrderQueryRepository : IQueryRepository<Order, int>
{
    Task<List<Order>> GetAllOrderByUserId(string userId);
    Task<Order> GetOrderDetails(string userId,int id);
}
