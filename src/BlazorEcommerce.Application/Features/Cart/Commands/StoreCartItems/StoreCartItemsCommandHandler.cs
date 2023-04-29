using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.Cart;

namespace BlazorEcommerce.Application.Features.Cart.Commands.StoreCartItems;

public record StoreCartItemsCommandRequest(List<CartItemDto> cartItems) : IRequest<IResponse>;

public class StoreCartItemsCommandHandler : IRequestHandler<StoreCartItemsCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;

    public StoreCartItemsCommandHandler(ICommandUnitOfWork<int> command, ICurrentUser currentUser, IMapper mapper)
    {
        _command = command;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(StoreCartItemsCommandRequest request, CancellationToken cancellationToken)
    {
        request.cartItems.ForEach(cartItem => cartItem.UserId = _currentUser.UserId);

        await _command.CartItemCommand.AddRangeAsync(_mapper.Map<List<CartItem>>(request.cartItems));
        await _command.SaveAsync();

        return new DataResponse<string?>(null);
    }
}
