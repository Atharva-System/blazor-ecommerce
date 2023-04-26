using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Application.UnitOfWork;
using BlazorEcommerce.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Persistence.UnitOfWork;

public class CommandUnitOfWork<Tkey> : ICommandUnitOfWork<Tkey>
{
    private readonly ICurrentUser _currentUser;
    private readonly PersistenceDataContext _context;

    public CommandUnitOfWork(PersistenceDataContext context, ICurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public CategoryCommandRepository _categoryCommand;
    public AddressCommandRepository _addressCommand;
    public CartItemCommandRepository _cartItemCommand;
    public ImageCommandRepository _imageCommand;
    public OrderCommandRepository _orderCommand;
    public OrderItemCommandRepository _orderItemCommand;
    public ProductCommandRepository _productCommand;
    public ProductTypeCommandRepository _productTypeCommand;
    public ProductVariantCommandRepository _productVariantCommand;

    public ICategoryCommandRepository CategoryCommand => _categoryCommand ?? (_categoryCommand = new CategoryCommandRepository(_context));

    public IAddressCommandRepository AddressCommand => _addressCommand ?? (_addressCommand = new AddressCommandRepository(_context));

    public ICartItemCommandRepository CartItemCommand => _cartItemCommand ?? (_cartItemCommand = new CartItemCommandRepository(_context));

    public IImageCommandRepository ImageCommand => _imageCommand ?? (_imageCommand = new ImageCommandRepository(_context));

    public IOrderCommandRepository OrderCommand => _orderCommand ?? (_orderCommand = new OrderCommandRepository(_context));

    public IOrderItemCommandRepository OrderItemCommand => _orderItemCommand ?? (_orderItemCommand = new OrderItemCommandRepository(_context));

    public IProductCommandRepository ProductCommand => _productCommand ?? (_productCommand = new ProductCommandRepository(_context));

    public IProductTypeCommandRepository ProductTypeCommand => _productTypeCommand ?? (_productTypeCommand = new ProductTypeCommandRepository(_context));

    public IProductVariantCommandRepository ProductVariantCommand => _productVariantCommand ?? (_productVariantCommand = new ProductVariantCommandRepository(_context));

    public async Task<int> SaveAsync()
    {
        AuditEntities(_context);
        return await _context.SaveChangesAsync();
    }

    private void AuditEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity<Tkey>>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentUser.UserId;
            }
            if (entry.State == EntityState.Added ||
                entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy = _currentUser.UserId;
                entry.Entity.LastModifiedUtc = DateTime.UtcNow;
            }
        }
    }
}
