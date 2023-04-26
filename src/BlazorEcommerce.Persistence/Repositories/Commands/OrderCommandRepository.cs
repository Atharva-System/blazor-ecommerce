namespace BlazorEcommerce.Persistence.Repositories.Commands
{
    public class OrderCommandRepository : CommandRepository<Order, int>, IOrderCommandRepository
    {
        public OrderCommandRepository(PersistenceDataContext context) : base(context)
        {
        }
    }
}
