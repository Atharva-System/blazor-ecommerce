﻿using BlazorEcommerce.Shared.Product;

namespace BlazorEcommerce.Application.Features.Product.Query.GetProductsByCategory;

public record GetProductsByCategoryQueryRequest(string categoryUrl) :IRequest<IResponse>;
public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly IMapper _mapper;

    public GetProductsByCategoryQueryHandler(IQueryUnitOfWork query, IMapper mapper)
    {
        _query = query;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(GetProductsByCategoryQueryRequest request, CancellationToken cancellationToken)
    {

        var products = await _query.ProductQuery.GetAllWithIncludeAsync(false,
                                p => p.IsActive && p.Category != null && p.Category.Url.ToLower().Equals(request.categoryUrl.ToLower()),
                                false,
                                p => p.Variants.Where(v => v.IsActive),
                                p => p.Images);

        return new DataResponse<List<ProductDto>>(_mapper.Map<List<ProductDto>>(products.ToList()), HttpStatusCodes.Accepted);
    }
}
