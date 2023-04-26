namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class ProductTypeQueryRepository : QueryRepository<ProductType, int>, IProductTypeQueryRepository
{
    public ProductTypeQueryRepository(PersistenceDataContext context) : base(context)
    {
    }
}
