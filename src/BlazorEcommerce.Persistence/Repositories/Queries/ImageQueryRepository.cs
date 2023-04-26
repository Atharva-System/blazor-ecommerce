namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class ImageQueryRepository : QueryRepository<Image, int>, IImageQueryRepository
{
    public ImageQueryRepository(PersistenceDataContext context) : base(context)
    {
    }
}
