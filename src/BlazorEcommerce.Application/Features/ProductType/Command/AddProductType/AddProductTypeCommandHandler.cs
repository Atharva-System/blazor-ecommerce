using BlazorEcommerce.Domain.Entities;
using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Application.Features.ProductType.Command.AddProductType;

public record AddProductTypeCommandRequest(ProductTypeDto productType) : IRequest;

public class AddProductTypeCommandHandler : IRequestHandler<AddProductTypeCommandRequest>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IMapper _mapper;

    public AddProductTypeCommandHandler(ICommandUnitOfWork<int> command, IMapper mapper)
    {
        _command = command;
        _mapper = mapper;
    }

    public async Task Handle(AddProductTypeCommandRequest request, CancellationToken cancellationToken)
    {
        var productType = _mapper.Map<Domain.Entities.ProductType>(request.productType);
        await _command.ProductTypeCommand.AddAsync(productType);
        await _command.SaveAsync();
    }
}
