using BlazorEcommerce.Shared.Product;

namespace BlazorEcommerce.Application.Repositories.Commands;

public interface IProductCommandRepository : ICommandRepository<Product, int>
{
}
