namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class ProductQueryRepository : QueryRepository<Product, int>, IProductQueryRepository
{
    public ProductQueryRepository(PersistenceDataContext context) : base(context)
    {
    }
}
