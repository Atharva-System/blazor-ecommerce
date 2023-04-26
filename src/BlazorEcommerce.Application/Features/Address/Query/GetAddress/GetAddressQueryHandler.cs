using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Application.Features.Address.Query.GetAddress;

public record GetAddressQueryRequest : IRequest<IResponse>;

public class GetAddressQueryHandler : IRequestHandler<GetAddressQueryRequest, IResponse>
{
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IQueryUnitOfWork _query;

    public GetAddressQueryHandler(IMapper mapper, ICurrentUser currentUser, IQueryUnitOfWork query)
    {
        _mapper = mapper;
        _currentUser = currentUser;
        _query = query;
    }

    public async Task<IResponse> Handle(GetAddressQueryRequest request, CancellationToken cancellationToken)
    {
        var address = await _query.AddressQuery.GetByIdAsync(cat => cat.UserId == _currentUser.UserId);
        if (address == null)
        {
            return new ErrorResponse(HttpStatusCodes.NotFound, String.Format(Messages.NotFound, "Address"));
        }

       var addressDto = _mapper.Map<AddressDto>(address);

        return new DataResponse<AddressDto>(addressDto, HttpStatusCodes.Accepted);
    }
}
