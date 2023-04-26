using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Category;

namespace BlazorEcommerce.Application.Features.Category.Query.GetCategories;

public record GetAllCategoryQueryRequest(bool forAdmin = false) : IRequest<ServiceResponse<List<CategoryDto>>>;

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, ServiceResponse<List<CategoryDto>>>
{
    private readonly IQueryUnitOfWork _query;
    private readonly IMapper _mapper;

    public GetAllCategoryQueryHandler(IQueryUnitOfWork query, IMapper mapper)
    {
        _query = query;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<CategoryDto>>> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        var result = new ServiceResponse<List<CategoryDto>>();

        if (request.forAdmin)
        {
            var categories = await _query.CategoryQuery.GetAllAsync(false);
            result.Data = _mapper.Map<List<CategoryDto>>(categories).ToList();
        }
        else
        {
            var categories = await _query.CategoryQuery.GetAllWithIncludeAsync(false, cat => cat.IsActive);
            result.Data = _mapper.Map<List<CategoryDto>>(categories).ToList();
        }

        return result;
    }
}
