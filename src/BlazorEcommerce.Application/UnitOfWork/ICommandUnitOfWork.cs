namespace BlazorEcommerce.Application.UnitOfWork;

public interface ICommandUnitOfWork<Tkey>
{
    IAddressCommandRepository AddressCommand { get; }
    ICartItemCommandRepository CartItemCommand { get; }
    ICategoryCommandRepository CategoryCommand { get; }
    IImageCommandRepository ImageCommand { get; }
    IOrderCommandRepository OrderCommand { get; }
    IOrderItemCommandRepository OrderItemCommand { get; }
    IProductCommandRepository ProductCommand { get; }
    IProductTypeCommandRepository ProductTypeCommand { get; }
    IProductVariantCommandRepository ProductVariantCommand { get; }
    Task<int> SaveAsync();
}
