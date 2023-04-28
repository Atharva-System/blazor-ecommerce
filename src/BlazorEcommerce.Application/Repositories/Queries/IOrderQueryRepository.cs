namespace BlazorEcommerce.Application.Repositories.Queries;

public interface IOrderQueryRepository : IQueryRepository<Order, int>
{
    Task<Order> GetOrderDetails(string userId,int id);
}
