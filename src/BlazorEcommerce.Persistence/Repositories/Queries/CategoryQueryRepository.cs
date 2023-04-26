namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class CategoryQueryRepository : QueryRepository<Category, int>, ICategoryQueryRepository
{
    public CategoryQueryRepository(PersistenceDataContext context) : base(context)
    {
    }
}
