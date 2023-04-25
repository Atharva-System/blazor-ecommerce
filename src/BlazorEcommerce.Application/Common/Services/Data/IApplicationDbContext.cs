using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Application.Common.Services.Data;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    //DbSet<TodoList> TodoLists { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
