using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Response.Concrete;

namespace BlazorEcommerce.Application.Features.Address.Command.AddAddress;

public record AddOrUpdateCommandRequest(AddressDto address) : IRequest<IResponse>;

public class AddOrUpdateAddressCommandHandler : IRequestHandler<AddOrUpdateCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IQueryUnitOfWork _query;

    public AddOrUpdateAddressCommandHandler(ICommandUnitOfWork<int> command, IMapper mapper, ICurrentUser currentUser, IQueryUnitOfWork query)
    {
        _command = command;
        _mapper = mapper;
        _currentUser = currentUser;
        _query = query;
    }

    public async Task<IResponse> Handle(AddOrUpdateCommandRequest request, CancellationToken cancellationToken)
    {
        var addressMapped = _mapper.Map<Domain.Entities.Address>(request.address);

        var isRecordExists = await _query.AddressQuery.AnyAsync(cat => cat.UserId == request.address.UserId);
        if (isRecordExists == false)
        {
            addressMapped.UserId = Convert.ToString(_currentUser.UserId);
            await _command.AddressCommand.AddAsync(addressMapped);
        }
        else
        {
            _command.AddressCommand.Update(addressMapped);
        }
        
        await _command.SaveAsync();

        return new DataResponse<string?>(null);
    }
}
