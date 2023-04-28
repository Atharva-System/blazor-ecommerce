using BlazorEcommerce.Shared.Product;

namespace BlazorEcommerce.Application.Features.Product.Query.SearchProducts;

public record SearchProductsQueryRequest(string searchText, int page) : IRequest<IResponse>;

public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly IMapper _mapper;

    public SearchProductsQueryHandler(IQueryUnitOfWork query, IMapper mapper)
    {
        _query = query;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(SearchProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var pageResults = 2f;

            var productSearchCount = (await _query.ProductQuery.GetAllWithIncludeAsync(false,
                                    p => p.Title.ToLower().Contains(request.searchText.ToLower()) ||
                                        p.Description.ToLower().Contains(request.searchText.ToLower()) &&
                                        p.IsActive,
                                    false,
                                    p => p.Variants)).Count();


        var pageCount = Math.Ceiling(productSearchCount / pageResults);

        var products = await _query.ProductQuery.GetAllWithIncludeAsync(false,
                                p => p.Title.ToLower().Contains(request.searchText.ToLower()) ||
                               p.Description.ToLower().Contains(request.searchText.ToLower()) &&
                               p.IsActive,
                                false,
                                p => p.Variants,
                                p => p.Images);

        var productLists = products.Skip((request.page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToList();

        var productSearchResult = new ProductSearchResult()
        {
            Products = _mapper.Map<List<ProductDto>>(productLists),
            CurrentPage = request.page,
            Pages = (int)pageCount
        };


        return new DataResponse<ProductSearchResult>(productSearchResult, HttpStatusCodes.Accepted);
    }
}
