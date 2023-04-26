using BlazorEcommerce.Application.UnitOfWork;

namespace BlazorEcommerce.Persistence.UnitOfWork;

public class QueryUnitOfWork : IQueryUnitOfWork
{
    private readonly PersistenceDataContext _context;

    public QueryUnitOfWork(PersistenceDataContext context)
    {
        _context = context;
    }

    public CategoryQueryRepository _categoryQuery;
    public AddressQueryRepository _addressQuery;
    public CartItemQueryRepository _cartItemQuery;
    public ImageQueryRepository _imageQuery;
    public OrderItemQueryRepository _orderItemQuery;
    public OrderQueryRepository _orderQuery;
    public ProductQueryRepository _productQuery;
    public ProductTypeQueryRepository _productTypeQuery;
    public ProductVariantQueryRepository _productVariantQuery;

    public ICategoryQueryRepository CategoryQuery => _categoryQuery ?? (_categoryQuery = new CategoryQueryRepository(_context));

    public IAddressQueryRepository AddressQuery => _addressQuery ?? (_addressQuery = new AddressQueryRepository(_context));

    public ICartItemQueryRepository CartItemQuery => _cartItemQuery ?? (_cartItemQuery = new CartItemQueryRepository(_context));

    public IImageQueryRepository ImageQuery => _imageQuery ?? (_imageQuery = new ImageQueryRepository(_context));

    public IOrderItemQueryRepository OrderItemQuery => _orderItemQuery ?? (_orderItemQuery = new OrderItemQueryRepository(_context));

    public IOrderQueryRepository OrderQuery => _orderQuery ?? (_orderQuery = new OrderQueryRepository(_context));

    public IProductQueryRepository ProductQuery => _productQuery ?? (_productQuery = new ProductQueryRepository(_context));

    public IProductTypeQueryRepository ProductTypeQuery => _productTypeQuery ?? (_productTypeQuery = new ProductTypeQueryRepository(_context));

    public IProductVariantQueryRepository ProductVariantQuery => _productVariantQuery ?? (_productVariantQuery = new ProductVariantQueryRepository(_context));
}
