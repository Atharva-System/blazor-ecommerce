using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Application.Features.Category.Query.GetCategories;

public record GetAllProductTypeQueryRequest : IRequest<IResponse>;

public class GetAllProductTypeQueryHandler : IRequestHandler<GetAllProductTypeQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly IMapper _mapper;

    public GetAllProductTypeQueryHandler(IQueryUnitOfWork query, IMapper mapper)
    {
        _query = query;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(GetAllProductTypeQueryRequest request, CancellationToken cancellationToken)
    {
        var productTypes = await _query.ProductTypeQuery.GetAllAsync(false);

        return new DataResponse<List<ProductTypeDto>>(_mapper.Map<List<ProductTypeDto>>(productTypes).ToList(), HttpStatusCodes.Accepted);
    }
}
