using BlazorEcommerce.Shared.Product;

namespace BlazorEcommerce.Application.Features.Product.Command.CreateProduct;

public record CreateProductCommandRequest(ProductDto product) : IRequest<IResponse>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(ICommandUnitOfWork<int> command, IMapper mapper)
    {
        _command = command;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        foreach (var variant in request.product.Variants)
        {
            variant.ProductType = null;
        }

        var product = _mapper.Map<Domain.Entities.Product>(request.product);

        await _command.ProductCommand.AddAsync(_mapper.Map<Domain.Entities.Product>(request.product));
        await _command.SaveAsync();

        return new DataResponse<ProductDto>(_mapper.Map<ProductDto>(product), HttpStatusCodes.Accepted);
    }
}
