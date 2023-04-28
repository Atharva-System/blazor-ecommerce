using BlazorEcommerce.Application.Contracts.Identity;

namespace BlazorEcommerce.Application.Features.Cart.Query.GetCartItemsCount;

public record GetCartItemsCountQueryRequest : IRequest<IResponse>;

public class GetCartItemsCountQueryHandler : IRequestHandler<GetCartItemsCountQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly ICurrentUser _currentUser;

    public GetCartItemsCountQueryHandler(IQueryUnitOfWork query, ICurrentUser currentUser)
    {
        _query = query;
        _currentUser = currentUser;
    }

    public async Task<IResponse> Handle(GetCartItemsCountQueryRequest request, CancellationToken cancellationToken)
    {
        var resords = await _query.CartItemQuery.GetAllWithIncludeAsync(false, ci => ci.UserId == _currentUser.UserId);

        return new DataResponse<int>(resords.Count(), HttpStatusCodes.Accepted);
    }
}
