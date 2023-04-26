namespace BlazorEcommerce.Persistence.Repositories.Commands
{
    public class ImageCommandRepository : CommandRepository<Image, int>, IImageCommandRepository
    {
        public ImageCommandRepository(PersistenceDataContext context) : base(context)
        {
        }
    }
}
