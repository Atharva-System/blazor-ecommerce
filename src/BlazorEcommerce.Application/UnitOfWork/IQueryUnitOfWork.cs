using BlazorEcommerce.Application.Repositories.Queries;

namespace BlazorEcommerce.Application.UnitOfWork;

public interface IQueryUnitOfWork
{
    ICategoryQueryRepository CategoryQuery { get; }
}
