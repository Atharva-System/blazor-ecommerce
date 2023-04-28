using BlazorEcommerce.Shared.Cart;

namespace BlazorEcommerce.Application.Features.Cart.Query.GetCartProducts;

public record GetCartProductsQueryRequest(List<CartItemDto> cartItems) : IRequest<IResponse>;

public class GetCartProductsQueryHandler : IRequestHandler<GetCartProductsQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly IMapper _mapper;

    public GetCartProductsQueryHandler(IQueryUnitOfWork query, IMapper mapper)
    {
        _query = query;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(GetCartProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var result = new List<CartProductResponse>();

        foreach (var item in request.cartItems)
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
                    v => v.ProductType);;

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

        return new DataResponse<List<CartProductResponse>>(result,HttpStatusCodes.Accepted);
    }
}
