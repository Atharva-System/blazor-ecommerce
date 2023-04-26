namespace BlazorEcommerce.Persistence.Repositories.Commands
{
    public class ProductCommandRepository : CommandRepository<Product, int>, IProductCommandRepository
    {
        public ProductCommandRepository(PersistenceDataContext context) : base(context)
        {
        }
    }
}
