namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class AddressQueryRepository : QueryRepository<Address, int>, IAddressQueryRepository
{
    public AddressQueryRepository(PersistenceDataContext context) : base(context)
    {
    }
}
