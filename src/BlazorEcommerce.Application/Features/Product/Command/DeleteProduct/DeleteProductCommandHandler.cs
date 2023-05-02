using BlazorEcommerce.Domain.Entities;

namespace BlazorEcommerce.Application.Features.Product.Command.DeleteProduct;

public record DeleteProductCommandRequest(int productId) : IRequest<IResponse>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IQueryUnitOfWork _query;

    public DeleteProductCommandHandler(ICommandUnitOfWork<int> command, IQueryUnitOfWork query)
    {
        _command = command;
        _query = query;
    }

    public async Task<IResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var dbProduct = await _query.ProductQuery.GetByIdAsync(p => p.Id == request.productId);

        if (dbProduct == null)
        {
            return new DataResponse<string?>(null, HttpStatusCodes.NotFound, String.Format(Messages.NotFound, "Product"), false);
        }

        dbProduct.IsDeleted = true;
        _command.ProductCommand.Update(dbProduct);
        await _command.SaveAsync();

        return new DataResponse<string?>(null);
    }
}
