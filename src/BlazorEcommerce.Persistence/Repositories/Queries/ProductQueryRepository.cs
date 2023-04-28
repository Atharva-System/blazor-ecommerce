using Azure.Core;
using BlazorEcommerce.Shared.Cart;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class ProductQueryRepository : QueryRepository<Product, int>, IProductQueryRepository
{
    public ProductQueryRepository(PersistenceDataContext context) : base(context)
    {
    }

   
}
