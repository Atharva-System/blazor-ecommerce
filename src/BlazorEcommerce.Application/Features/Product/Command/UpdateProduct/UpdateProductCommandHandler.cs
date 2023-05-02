using BlazorEcommerce.Shared.Product;

namespace BlazorEcommerce.Application.Features.Product.Command.UpdateProduct;

public record UpdateProductCommandRequest(ProductDto product) : IRequest<IResponse>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IQueryUnitOfWork _query;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(ICommandUnitOfWork<int> command, IQueryUnitOfWork query, IMapper mapper)
    {
        _command = command;
        _query = query;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var dbProduct = await _query.ProductQuery.GetWithIncludeAsync(false,
                            p => p.Id == request.product.Id,
                            false,
                            p => p.Images);

        if (dbProduct == null)
        {
            return new DataResponse<string?>(null, HttpStatusCodes.NotFound, String.Format(Messages.NotFound, "Product"), false);
        }

        dbProduct.Title = request.product.Title;
        dbProduct.Description = request.product.Description;
        dbProduct.ImageUrl = request.product.ImageUrl;
        dbProduct.CategoryId = request.product.CategoryId;
        dbProduct.Featured = request.product.Featured;

        var productImages = dbProduct.Images;
        _command.ImageCommand.RemoveRange(productImages);

        dbProduct.Images = _mapper.Map<List<Image>>(request.product.Images);

        foreach (var variant in request.product.Variants)
        {
            var dbVariant = await _query.ProductVariantQuery.GetAsync(v => v.ProductId == variant.ProductId &&
                    v.ProductTypeId == variant.ProductTypeId);

            if (dbVariant == null)
            {
                variant.ProductType = null;
                await _command.ProductVariantCommand.AddAsync(_mapper.Map<ProductVariant>(variant));
            }
            else
            {
                dbVariant.ProductTypeId = variant.ProductTypeId;
                dbVariant.Price = variant.Price;
                dbVariant.OriginalPrice = variant.OriginalPrice;
                dbVariant.IsActive = variant.IsActive;
                dbVariant.IsDeleted = variant.IsDeleted;
                _command.ProductVariantCommand.Update(dbVariant);
            }
        }

        _command.ProductCommand.Update(dbProduct);
        await _command.SaveAsync();

        return new DataResponse<ProductDto>(request.product, HttpStatusCodes.Accepted);
    }
}
