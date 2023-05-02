using BlazorEcommerce.Application.Repositories.Queries.Base;
using BlazorEcommerce.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlazorEcommerce.Persistence.Repositories.Queries.Base;

public class QueryRepository<T, TKey> : IQueryRepository<T, TKey> where T : BaseEntity<TKey>, new()
{
    protected readonly PersistenceDataContext context;

    public QueryRepository(PersistenceDataContext context)
    {
        this.context = context;
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false)
    {
        if (ignoreQueryFilters)
        {
            return await context.Set<T>().IgnoreQueryFilters().Where(predicate).AnyAsync();
        }
        else
        {
            return await context.Set<T>().Where(predicate).AnyAsync();
        }
    }

    public async Task<IQueryable<T>> GetAllAsync(bool isChangeTracking = false, bool ignoreQueryFilters = false)
    {
        IQueryable<T> query = context.Set<T>();
        if (ignoreQueryFilters)
        {
            if (!isChangeTracking)
            {
                return query = query.IgnoreQueryFilters().AsNoTracking().AsQueryable();
            }
            return await Task.Run(() => query.IgnoreQueryFilters().AsQueryable());
        }
        else
        {
            if (!isChangeTracking)
            {
                return query = query.AsNoTracking().AsQueryable();
            }
            return await Task.Run(() => query.AsQueryable());
        }
    }

    public async Task<IEnumerable<T>> GetAllWithIncludeAsync(bool isChangeTracking = false, Expression<Func<T, bool>> predicate = null, bool ignoreQueryFilters = false, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = context.Set<T>();

        if (ignoreQueryFilters)
        {
            if (!isChangeTracking)
            {
                query = predicate is null ? query.IgnoreQueryFilters().AsNoTracking()
                                          : query.IgnoreQueryFilters().Where(predicate).AsNoTracking();
                if (includes != null)
                {
                    foreach (var item in includes)
                    {
                        query = query.Include(item);
                    }
                }
            }
            else
            {
                query = predicate is null ? query.IgnoreQueryFilters()
                                          : query.IgnoreQueryFilters().Where(predicate);
                if (includes != null)
                {
                    foreach (var item in includes)
                    {
                        query = query.Include(item);
                    }
                }
            }
        }
        else
        {
            if (!isChangeTracking)
            {
                query = predicate is null ? query.AsNoTracking()
                                          : query.Where(predicate).AsNoTracking();
                if (includes != null)
                {
                    foreach (var item in includes)
                    {
                        query = query.Include(item);
                    }
                }
            }
            else
            {
                query = predicate is null ? query
                                          : query.Where(predicate);
                if (includes != null)
                {
                    foreach (var item in includes)
                    {
                        query = query.Include(item);
                    }
                }
            }
        }
        return await query.ToListAsync();

    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool isChangeTracking = false, bool ignoreQueryFilters = false)
    {
        IQueryable<T> query = context.Set<T>();
        if (ignoreQueryFilters)
        {
            if (!isChangeTracking)
            {
                query = query.IgnoreQueryFilters().Where(predicate).AsNoTracking();
            }
            else
            {
                query = query.IgnoreQueryFilters().Where(predicate);
            }
        }
        else
        {
            if (!isChangeTracking)
            {
                query = query.Where(predicate).AsNoTracking();
            }
            else
            {
                query = query.Where(predicate);
            }
        }
       
        return await query.FirstOrDefaultAsync();
    }

    public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, bool isChangeTracking = false, bool ignoreQueryFilters = false)
    {
        IQueryable<T> query = context.Set<T>();
        if (ignoreQueryFilters)
        {
            if (!isChangeTracking)
            {
                query = query.IgnoreQueryFilters().Where(predicate).AsNoTracking();
            }
            else
            {
                query = query.IgnoreQueryFilters().Where(predicate);
            }
        }
        else
        {
            if (!isChangeTracking)
            {
                query = query.Where(predicate).AsNoTracking();
            }
            else
            {
                query = query.Where(predicate);
            }
        }

        return await query.SingleOrDefaultAsync();
    }

    public async Task<T> GetWithIncludeAsync(bool isChangeTracking, Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = context.Set<T>();
        if (ignoreQueryFilters)
        {
            if (!isChangeTracking)
            {
                query = query.IgnoreQueryFilters().Where(predicate);
                if (includes is not null)
                {
                    foreach (var item in includes)
                    {
                        query = query.Include(item);
                    }
                }
            }
            else
            {
                query = query.IgnoreQueryFilters().Where(predicate).AsNoTracking();
                if (includes is not null)
                {
                    foreach (var item in includes)
                    {
                        query = query.Include(item);
                    }
                }
            }
        }
        else
        {
            if (!isChangeTracking)
            {
                query = query.Where(predicate);
                if (includes is not null)
                {
                    foreach (var item in includes)
                    {
                        query = query.Include(item);
                    }
                }
            }
            else
            {
                query = query.Where(predicate).AsNoTracking();
                if (includes is not null)
                {
                    foreach (var item in includes)
                    {
                        query = query.Include(item);
                    }
                }
            }
        }

        return await query.FirstOrDefaultAsync();

    }
}
