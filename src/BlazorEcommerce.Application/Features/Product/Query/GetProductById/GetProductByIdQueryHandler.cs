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

        product = await _query.ProductQuery.GetProductByIdAsync(request.productId, _currentUser.UserIsInRole(Constants.AdminRoleName));

        if (product == null)
        {
            return new DataResponse<string?>(null, HttpStatusCodes.NotFound, String.Format(Messages.NotExist, "Product"), false);
        }
        else
        {
            return new DataResponse<ProductDto>(_mapper.Map<ProductDto>(product), HttpStatusCodes.Accepted);

        }

    }
}
