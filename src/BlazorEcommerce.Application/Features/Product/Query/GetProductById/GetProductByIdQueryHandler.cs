using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.Product;

namespace BlazorEcommerce.Application.Features.Product.Query.GetProductById;

public record GetProductByIdQueryRequest(int productId) : IRequest<IResponse>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public GetProductByIdQueryHandler(IQueryUnitOfWork query, IMapper mapper, ICurrentUser currentUser)
    {
        _query = query;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<IResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product product = null;

        if (_currentUser.UserIsInRole(Constants.AdminRoleName))
        {
            product = await _query.ProductQuery.GetWithIncludeAsync(false,
                                p => p.Id == request.productId,
                                false,
                                p => p.Variants.Select(v => v.ProductType),
                                p => p.Images);
        }
        else
        {
            product = await _query.ProductQuery.GetWithIncludeAsync(false,
                                p => p.Id == request.productId && p.IsActive,
                                false,
                                p => p.Variants.Where(v => v.IsActive).Select(v => v.ProductType),
                                p => p.Images);

        }

        if (product == null)
        {
            return new ErrorResponse(HttpStatusCodes.NotFound, String.Format(Messages.NotExist, "Product"));
        }
        else
        {
            return new DataResponse<ProductDto>(_mapper.Map<ProductDto>(product), HttpStatusCodes.Accepted);

        }

    }
}
