using BlazorEcommerce.Shared.Product;

namespace BlazorEcommerce.Application.Features.Product.Query.GetFeaturedProducts;

public record GetFeaturedProductsQueryRequest : IRequest<IResponse>;

public class GetFeaturedProductsQueryHandler : IRequestHandler<GetFeaturedProductsQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly IMapper _mapper;

    public GetFeaturedProductsQueryHandler(IQueryUnitOfWork query, IMapper mapper)
    {
        _query = query;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(GetFeaturedProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _query.ProductQuery.GetAllWithIncludeAsync(false,
                                p => p.IsActive && p.Featured,
                                false,
                                p => p.Variants.Where(v => v.IsActive),
                                p => p.Images);

        return new DataResponse<List<ProductDto>>(_mapper.Map<List<ProductDto>>(products.ToList()), HttpStatusCodes.Accepted);
    }
}
