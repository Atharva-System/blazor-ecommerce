namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class ProductVariantQueryRepository : QueryRepository<ProductVariant, int>, IProductVariantQueryRepository
{
    public ProductVariantQueryRepository(PersistenceDataContext context) : base(context)
    {
    }
}
