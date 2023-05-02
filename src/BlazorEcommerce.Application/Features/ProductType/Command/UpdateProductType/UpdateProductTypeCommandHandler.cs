using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Application.Features.Category.Commands.UpdateCategory;

public record UpdateProductTypeCommandRequest(ProductTypeDto productType) : IRequest<IResponse>;

public class DeleteProductTypeCommandHandler : IRequestHandler<UpdateProductTypeCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IMapper _mapper;
    private readonly IQueryUnitOfWork _query;

    public DeleteProductTypeCommandHandler(ICommandUnitOfWork<int> command, IMapper mapper, IQueryUnitOfWork query)
    {
        _command = command;
        _mapper = mapper;
        _query = query;
    }

    public async Task<IResponse> Handle(UpdateProductTypeCommandRequest request, CancellationToken cancellationToken)
    {
        var productType = await _query.ProductTypeQuery.GetAsync(p => p.Id == request.productType.Id);
        if (productType == null)
        {
            return new DataResponse<string?>(null, HttpStatusCodes.NotFound, String.Format(Messages.NotFound, "Product Type"), false);
        }

        productType = _mapper.Map<Domain.Entities.ProductType>(request.productType);

        if (productType == null)
        {
            return new DataResponse<string?>(null, HttpStatusCodes.NotFound, String.Format(Messages.NotFound, "Product Type"), false);
        }

        _command.ProductTypeCommand.Update(productType);
        await _command.SaveAsync();

        return new DataResponse<string?>(null);
    }
}
