using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.Cart;

namespace BlazorEcommerce.Application.Features.Cart.Commands.UpdateQuantity;

public record UpdateQuantityCommandRequest(CartItemDto cartItem) : IRequest<IResponse>;

public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IQueryUnitOfWork _query;
    private readonly ICurrentUser _currentUser;

    public UpdateQuantityCommandHandler(ICommandUnitOfWork<int> command, IQueryUnitOfWork query, ICurrentUser currentUser)
    {
        _command = command;
        _query = query;
        _currentUser = currentUser;
    }

    public async Task<IResponse> Handle(UpdateQuantityCommandRequest request, CancellationToken cancellationToken)
    {
        var dbCartItem = await _query.CartItemQuery.GetByIdAsync(ci => ci.ProductId == request.cartItem.ProductId &&
               ci.ProductTypeId == request.cartItem.ProductTypeId && ci.UserId == _currentUser.UserId);

        if (dbCartItem == null)
        {
            return new DataResponse<string?>(null, HttpStatusCodes.NotFound,String.Format(Messages.NotExist, "Cart item"), false);
        }

        dbCartItem.Quantity = request.cartItem.Quantity;
        _command.CartItemCommand.Update(dbCartItem);
        await _command.SaveAsync();

        return new DataResponse<string?>(null);
    }
}
