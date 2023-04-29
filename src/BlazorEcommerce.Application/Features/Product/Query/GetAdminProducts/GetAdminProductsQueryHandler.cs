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
        IList<Domain.Entities.Product> source = await _query.ProductQuery.GetAllAdminProductAsync();

        return new DataResponse<List<ProductDto>>(_mapper.Map<List<ProductDto>>(source), HttpStatusCodes.Accepted);
    }
}
