namespace BlazorEcommerce.Application.UnitOfWork;

public interface ICommandUnitOfWork<Tkey>
{
    ICategoryCommandRepository CategoryCommand { get; }
    Task<int> SaveAsync();
}
