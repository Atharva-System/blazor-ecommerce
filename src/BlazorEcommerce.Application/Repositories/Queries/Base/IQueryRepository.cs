using BlazorEcommerce.Domain.Common;
using System.Linq.Expressions;

namespace BlazorEcommerce.Application.Repositories.Queries.Base;

public interface IQueryRepository<T,Tkey> where T : BaseEntity<Tkey>, new()
{
    Task<IQueryable<T>> GetAllAsync(bool isChangeTracking = false, bool ignoreQueryFilters = false);
    Task<IEnumerable<T>> GetAllWithIncludeAsync(bool isChangeTracking = false, Expression<Func<T, bool>> predicate = null, bool ignoreQueryFilters = false, params Expression<Func<T, object>>[] includes);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool isChangeTracking = false, bool ignoreQueryFilters = false);
    Task<T> GetWithIncludeAsync(bool isChangeTracking, Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false, params Expression<Func<T, object>>[] includes);
    Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, bool isChangeTracking = false, bool ignoreQueryFilters = false);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false);

}
