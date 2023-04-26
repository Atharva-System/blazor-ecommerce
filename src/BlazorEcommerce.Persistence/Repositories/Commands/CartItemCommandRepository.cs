namespace BlazorEcommerce.Persistence.Repositories.Commands
{
    public class CartItemCommandRepository : CommandRepository<CartItem, int>, ICartItemCommandRepository
    {
        public CartItemCommandRepository(PersistenceDataContext context) : base(context)
        {
        }
    }
}
