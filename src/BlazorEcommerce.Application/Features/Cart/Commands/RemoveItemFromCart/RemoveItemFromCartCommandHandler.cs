using BlazorEcommerce.Application.Contracts.Identity;

namespace BlazorEcommerce.Application.Features.Cart.Commands.RemoveItemFromCart;

public record RemoveItemFromCartCommandRequest(int productId, int productTypeId) : IRequest<IResponse>;

public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IQueryUnitOfWork _query;
    private readonly ICurrentUser _currentUser;

    public RemoveItemFromCartCommandHandler(ICommandUnitOfWork<int> command, IQueryUnitOfWork query, ICurrentUser currentUser)
    {
        _command = command;
        _query = query;
        _currentUser = currentUser;
    }

    public async Task<IResponse> Handle(RemoveItemFromCartCommandRequest request, CancellationToken cancellationToken)
    {
        var dbCartItem = await _query.CartItemQuery.GetByIdAsync(ci => ci.ProductId == request.productId &&
                ci.ProductTypeId == request.productTypeId && ci.UserId == _currentUser.UserId);

        if (dbCartItem == null)
        {
            return new DataResponse<string?>(null, HttpStatusCodes.NotFound, String.Format(Messages.NotExist, "Cart item"), false);
        }

        _command.CartItemCommand.Remove(dbCartItem);
        await _command.SaveAsync();

        return new DataResponse<string?>(null);
    }
}
