using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Application.Features.Address.Query.GetAddress;

public record GetAddressQueryRequest : IRequest<IResponse>;

public class GetAddressQueryHandler : IRequestHandler<GetAddressQueryRequest, IResponse>
{
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IQueryUnitOfWork _query;
    private readonly IIdentityService _identityService;

    public GetAddressQueryHandler(IMapper mapper, ICurrentUser currentUser, IQueryUnitOfWork query, IIdentityService identityService)
    {
        _mapper = mapper;
        _currentUser = currentUser;
        _query = query;
        _identityService = identityService;
    }

    public async Task<IResponse> Handle(GetAddressQueryRequest request, CancellationToken cancellationToken)
    {
        var address = await _query.AddressQuery.GetByIdAsync(cat => cat.UserId == _currentUser.UserId);
        if (address == null)
        {
            address = new Domain.Entities.Address();
        }

        var addressDto = _mapper.Map<AddressDto>(address);

        if (addressDto != null && (string.IsNullOrEmpty(addressDto.FirstName) || string.IsNullOrEmpty(addressDto.LastName)))
        {
            if (_currentUser.IsUserAuthenticated)
            {
                var user = await _identityService.GetUserAsync(_currentUser.UserId);

                if (user != null)
                {
                    if (string.IsNullOrEmpty(addressDto.FirstName))
                    {
                        addressDto.FirstName = user.FirstName;
                    }

                    if (string.IsNullOrEmpty(addressDto.LastName))
                    {
                        addressDto.LastName = user.LastName;
                    }
                }
            }
        }

        return new DataResponse<AddressDto>(addressDto, HttpStatusCodes.Accepted);
    }
}
