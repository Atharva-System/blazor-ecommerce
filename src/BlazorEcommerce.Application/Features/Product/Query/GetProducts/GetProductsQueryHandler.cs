using BlazorEcommerce.Shared.Product;

namespace BlazorEcommerce.Application.Features.Product.Query.GetProducts;

public record GetProductsQueryRequest : IRequest<IResponse>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IQueryUnitOfWork query, IMapper mapper)
    {
        _query = query;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _query.ProductQuery.GetAllWithIncludeAsync(false,
                                p => p.IsActive,
                                false,
                                p => p.Variants.Where(v => v.IsActive),
                                p => p.Images);

        return new DataResponse<List<ProductDto>>(_mapper.Map<List<ProductDto>>(products.ToList()), HttpStatusCodes.Accepted);
    }
}
