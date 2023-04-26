namespace BlazorEcommerce.Application.UnitOfWork;

public interface IQueryUnitOfWork
{
    IAddressQueryRepository AddressQuery { get; }
    ICartItemQueryRepository CartItemQuery { get; }
    ICategoryQueryRepository CategoryQuery { get; }
    IImageQueryRepository ImageQuery { get; }
    IOrderItemQueryRepository OrderItemQuery { get; }
    IOrderQueryRepository OrderQuery { get; }
    IProductQueryRepository ProductQuery { get; }
    IProductTypeQueryRepository ProductTypeQuery { get; }
    IProductVariantQueryRepository ProductVariantQuery { get; }
}
