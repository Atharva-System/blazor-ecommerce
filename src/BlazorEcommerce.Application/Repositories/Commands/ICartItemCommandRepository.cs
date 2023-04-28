using BlazorEcommerce.Shared.Cart;

namespace BlazorEcommerce.Application.Repositories.Commands;

public interface ICartItemCommandRepository : ICommandRepository<CartItem, int>
{
}
