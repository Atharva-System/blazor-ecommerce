namespace BlazorEcommerce.Persistence.Repositories.Commands
{
    public class CategoryCommandRepository : CommandRepository<Category, int>, ICategoryCommandRepository
    {
        public CategoryCommandRepository(PersistenceDataContext context) : base(context)
        {
        }
    }
}
