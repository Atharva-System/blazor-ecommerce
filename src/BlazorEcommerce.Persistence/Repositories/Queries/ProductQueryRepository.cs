using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlazorEcommerce.Persistence.Repositories.Queries;

public class ProductQueryRepository : QueryRepository<Product, int>, IProductQueryRepository
{
    public ProductQueryRepository(PersistenceDataContext context) : base(context)
    {
    }

    public async Task<IList<Product>> GetAllAdminProductAsync()
    {
        return await context.Products
            .Include(p => p.Images)
            .Include(p => p.Variants).ThenInclude(p => p.ProductType)
            .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id, bool isAdminRole)
    {
        if (isAdminRole)
        {
            return await context.Products
                .Include(p => p.Images)
                .Include(p => p.Variants).ThenInclude(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        else
        {
            return await context.Products
                .Include(p => p.Images)
                .Include(p => p.Variants).ThenInclude(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        };
    }
}
