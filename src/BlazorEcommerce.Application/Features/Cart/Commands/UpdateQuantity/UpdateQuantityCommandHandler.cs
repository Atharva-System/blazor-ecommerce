using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.Cart;

namespace BlazorEcommerce.Application.Features.Cart.Commands.UpdateQuantity;

public record UpdateQuantityCommandRequest(CartItemDto cartItem) : IRequest<IResponse>;

public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IQueryUnitOfWork _query;
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;

    public UpdateQuantityCommandHandler(ICommandUnitOfWork<int> command, IQueryUnitOfWork query, ICurrentUser currentUser, IMapper mapper)
    {
        _command = command;
        _query = query;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(UpdateQuantityCommandRequest request, CancellationToken cancellationToken)
    {
        var dbCartItem = await _query.CartItemQuery.GetByIdAsync(ci => ci.ProductId == request.cartItem.ProductId &&
               ci.ProductTypeId == request.cartItem.ProductTypeId && ci.UserId == _currentUser.UserId);

        if (dbCartItem == null)
        {
            return new ErrorResponse(HttpStatusCodes.NotFound,String.Format(Messages.NotExist, "Cart item"));
        }

        dbCartItem.Quantity = request.cartItem.Quantity;
        await _command.SaveAsync();

        return new SuccessResponse();
    }
}
