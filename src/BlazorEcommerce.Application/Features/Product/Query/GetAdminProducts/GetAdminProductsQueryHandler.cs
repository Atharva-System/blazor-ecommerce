using BlazorEcommerce.Shared.Product;

namespace BlazorEcommerce.Application.Features.Product.Query.GetAdminProducts;

public record GetAdminProductsQueryRequest : IRequest<IResponse>;

public class GetAdminProductsQueryHandler : IRequestHandler<GetAdminProductsQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly IMapper _mapper;

    public GetAdminProductsQueryHandler(IQueryUnitOfWork query, IMapper mapper)
    {
        _query = query;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(GetAdminProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _query.ProductQuery.GetAllWithIncludeAsync(false,
                                 null,
                                 false,
                                 p => p.Variants.Select(v => v.ProductType),
                                 p => p.Images);

        return new DataResponse<List<ProductDto>>(_mapper.Map<List<ProductDto>>(products.ToList()), HttpStatusCodes.Accepted);
    }
}
