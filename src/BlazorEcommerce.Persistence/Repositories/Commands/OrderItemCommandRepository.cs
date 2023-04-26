namespace BlazorEcommerce.Persistence.Repositories.Commands
{
    public class OrderItemCommandRepository : CommandRepository<OrderItem, int>, IOrderItemCommandRepository
    {
        public OrderItemCommandRepository(PersistenceDataContext context) : base(context)
        {
        }
    }
}
