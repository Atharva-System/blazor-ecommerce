namespace BlazorEcommerce.Persistence.Repositories.Commands
{
    public class ProductTypeCommandRepository : CommandRepository<ProductType, int>, IProductTypeCommandRepository
    {
        public ProductTypeCommandRepository(PersistenceDataContext context) : base(context)
        {
        }
    }
}
