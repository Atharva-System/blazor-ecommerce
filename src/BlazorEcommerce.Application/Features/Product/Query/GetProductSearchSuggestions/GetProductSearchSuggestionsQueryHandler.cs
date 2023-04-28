namespace BlazorEcommerce.Application.Features.Product.Query.GetProductSearchSuggestions;

public record GetProductSearchSuggestionsQueryRequest(string searchText) : IRequest<IResponse>;

public class GetProductSearchSuggestionsQueryHandler : IRequestHandler<GetProductSearchSuggestionsQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;

    public GetProductSearchSuggestionsQueryHandler(IQueryUnitOfWork query)
    {
        _query = query;
    }

    public async Task<IResponse> Handle(GetProductSearchSuggestionsQueryRequest request, CancellationToken cancellationToken)
    {
        var productSearch = await _query.ProductQuery.GetAllWithIncludeAsync(false,
                               p => p.Title.ToLower().Contains(request.searchText.ToLower()) ||
                                   p.Description.ToLower().Contains(request.searchText.ToLower()) &&
                                   p.IsActive,
                               false,
                               p => p.Variants);

        List<string> result = new List<string>();

        foreach (var product in productSearch)
        {
            if (product.Title.Contains(request.searchText, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(product.Title);
            }

            if (product.Description != null)
            {
                var punctuation = product.Description.Where(char.IsPunctuation)
                    .Distinct().ToArray();
                var words = product.Description.Split()
                    .Select(s => s.Trim(punctuation));

                foreach (var word in words)
                {
                    if (word.Contains(request.searchText, StringComparison.OrdinalIgnoreCase)
                        && !result.Contains(word))
                    {
                        result.Add(word);
                    }
                }
            }
        }

        return new DataResponse<List<string>>(result, HttpStatusCodes.Accepted);
    }
}
