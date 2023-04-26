using BlazorEcommerce.Shared.Category;

namespace BlazorEcommerce.Application.Features.Category.Query.GetCategories;

public record GetAllCategoryQueryRequest(bool forAdmin = false) : IRequest<IResponse>;

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly IMapper _mapper;

    public GetAllCategoryQueryHandler(IQueryUnitOfWork query, IMapper mapper)
    {
        _query = query;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        var categoryList  = new List<CategoryDto>();

        if (request.forAdmin)
        {
            var categories = await _query.CategoryQuery.GetAllAsync(false);
            categoryList = _mapper.Map<List<CategoryDto>>(categories).ToList();
        }
        else
        {
            var categories = await _query.CategoryQuery.GetAllWithIncludeAsync(false, cat => cat.IsActive);
            categoryList = _mapper.Map<List<CategoryDto>>(categories).ToList();
        }

        return new DataResponse<List<CategoryDto>>(categoryList, HttpStatusCodes.Accepted);
    }
}
