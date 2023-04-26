namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class OrderItemQueryRepository : QueryRepository<OrderItem, int>, IOrderItemQueryRepository
{
    public OrderItemQueryRepository(PersistenceDataContext context) : base(context)
    {
    }
}
