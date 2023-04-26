namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class OrderQueryRepository : QueryRepository<Order, int>, IOrderQueryRepository
{
    public OrderQueryRepository(PersistenceDataContext context) : base(context)
    {
    }
}
