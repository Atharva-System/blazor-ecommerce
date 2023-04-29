namespace BlazorEcommerce.Application.Repositories.Queries;

public interface IProductQueryRepository : IQueryRepository<Domain.Entities.Product, int>
{
    Task<Product> GetProductByIdAsync(int id, bool isAdminRole);

    Task<IList<Product>> GetAllAdminProductAsync();
}
