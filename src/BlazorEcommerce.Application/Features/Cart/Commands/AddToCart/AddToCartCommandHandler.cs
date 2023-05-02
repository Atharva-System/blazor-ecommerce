using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.Cart;

namespace BlazorEcommerce.Application.Features.Cart.Commands.AddToCart;

public record AddToCartCommandRequest(CartItemDto cartItem) : IRequest<IResponse>;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IQueryUnitOfWork _query;
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;

    public AddToCartCommandHandler(ICommandUnitOfWork<int> command, IQueryUnitOfWork query, ICurrentUser currentUser, IMapper mapper)
    {
        _command = command;
        _query = query;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(AddToCartCommandRequest request, CancellationToken cancellationToken)
    {
        request.cartItem.UserId = _currentUser.UserId;

        var sameItem = await _query.CartItemQuery.GetByIdAsync(ci => ci.ProductId == request.cartItem.ProductId &&
            ci.ProductTypeId == request.cartItem.ProductTypeId && ci.UserId == request.cartItem.UserId);

        if (sameItem == null)
        {
            await _command.CartItemCommand.AddAsync(_mapper.Map<CartItem>(request.cartItem));
        }
        else
        {
            sameItem.Quantity += request.cartItem.Quantity;
            _command.CartItemCommand.Update(sameItem);
        }
        
        await _command.SaveAsync();

        return new DataResponse<string?>(null);
    }
}
