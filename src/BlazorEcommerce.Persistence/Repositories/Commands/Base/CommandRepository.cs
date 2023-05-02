using BlazorEcommerce.Application.Repositories.Commands.Base;
using BlazorEcommerce.Domain.Common;

namespace BlazorEcommerce.Persistence.Repositories.Commands.Base;

public class CommandRepository<T,TKey> : ICommandRepository<T, TKey> where T : BaseEntity<TKey>, new()
{
    protected readonly PersistenceDataContext context;

    public CommandRepository(PersistenceDataContext context)
    {
        this.context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await context.Set<T>().AddRangeAsync(entities);
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        context.Set<T>().RemoveRange(entities);
    }

    public T Update(T entity)
    {
        context.Set<T>().Update(entity);
        return entity;
    }
}
