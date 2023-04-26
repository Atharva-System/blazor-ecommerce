namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class CartItemQueryRepository : QueryRepository<CartItem, int>, ICartItemQueryRepository
{
    public CartItemQueryRepository(PersistenceDataContext context) : base(context)
    {
    }
}
