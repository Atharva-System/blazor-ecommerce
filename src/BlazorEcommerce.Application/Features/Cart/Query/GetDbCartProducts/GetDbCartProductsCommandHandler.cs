using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.Cart;

namespace BlazorEcommerce.Application.Features.Cart.Query.GetDbCartProducts;

public record GetDbCartProductsQueryRequest : IRequest<IResponse>;

public class GetDbCartProductsQueryHandler : IRequestHandler<GetDbCartProductsQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly ICurrentUser _currentUser;

    public GetDbCartProductsQueryHandler(IQueryUnitOfWork query, ICurrentUser currentUser)
    {
        _query = query;
        _currentUser = currentUser;
    }

    public async Task<IResponse> Handle(GetDbCartProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var cartItems = await _query.CartItemQuery.GetAllWithIncludeAsync(false,ci => ci.UserId == _currentUser.UserId);

        var result = new List<CartProductResponse>();

        foreach (var item in cartItems)
        {
            var product = await _query.ProductQuery.GetByIdAsync(p => p.Id == item.ProductId);

            if (product == null)
            {
                continue;
            }

            var productVariant = await _query.ProductVariantQuery.GetWithIncludeAsync(false,
                    v => v.ProductId == item.ProductId
                    && v.ProductTypeId == item.ProductTypeId,
                    false,
                    v => v.ProductType); ;

            if (productVariant == null)
            {
                continue;
            }

            var cartProduct = new CartProductResponse
            {
                ProductId = product.Id,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Price = productVariant.Price,
                ProductType = productVariant.ProductType.Name,
                ProductTypeId = productVariant.ProductTypeId,
                Quantity = item.Quantity
            };

            result.Add(cartProduct);
        }

        return new DataResponse<List<CartProductResponse>>(result, HttpStatusCodes.Accepted);
    }
}
