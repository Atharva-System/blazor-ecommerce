using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Http;

namespace BlazorEcommerce.Application.Features.Category.Commands.DeleteCategory;

public record DeleteCategoryCommandRequest(int id) : IRequest<ServiceResponse<bool>>;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, ServiceResponse<bool>>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IQueryUnitOfWork _query;

    public DeleteCategoryCommandHandler(ICommandUnitOfWork<int> command, IQueryUnitOfWork query)
    {
        _command = command;
        _query = query;
    }

    public async Task<ServiceResponse<bool>> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var category = await _query.CategoryQuery.GetByIdAsync(cat => cat.Id == request.id);
        if (category == null)
        {
            return new ServiceResponse<bool>() { Success = false, Message = "Category is not found!", StatusCode = StatusCodes.Status404NotFound };
        }

        category.IsDeleted = true;
        _command.CategoryCommand.Update(category);
        await _command.SaveAsync();

        return new ServiceResponse<bool>() { Success = true, StatusCode = StatusCodes.Status200OK };
    }
}
