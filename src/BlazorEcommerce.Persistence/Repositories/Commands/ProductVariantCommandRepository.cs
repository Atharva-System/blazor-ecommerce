namespace BlazorEcommerce.Persistence.Repositories.Commands
{
    public class ProductVariantCommandRepository : CommandRepository<ProductVariant, int>, IProductVariantCommandRepository
    {
        public ProductVariantCommandRepository(PersistenceDataContext context) : base(context)
        {
        }
    }
}
