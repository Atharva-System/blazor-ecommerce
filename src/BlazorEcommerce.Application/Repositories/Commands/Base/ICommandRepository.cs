using BlazorEcommerce.Domain.Common;

namespace BlazorEcommerce.Application.Repositories.Commands.Base;

public interface ICommandRepository<T,Tkey> where T : BaseEntity<Tkey>, new()
{
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    T Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
