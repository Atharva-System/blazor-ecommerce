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

    public ICategoryCommandRepository CategoryCommand => _categoryCommand ?? (_categoryCommand = new CategoryCommandRepository(_context));

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
