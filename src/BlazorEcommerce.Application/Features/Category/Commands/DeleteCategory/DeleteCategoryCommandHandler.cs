namespace BlazorEcommerce.Application.Features.Category.Commands.DeleteCategory;

public record DeleteCategoryCommandRequest(int id) : IRequest<IResponse>;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IQueryUnitOfWork _query;

    public DeleteCategoryCommandHandler(ICommandUnitOfWork<int> command, IQueryUnitOfWork query)
    {
        _command = command;
        _query = query;
    }

    public async Task<IResponse> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var category = await _query.CategoryQuery.GetByIdAsync(cat => cat.Id == request.id);
        if (category == null)
        {
            return new DataResponse<string?>(null, HttpStatusCodes.NotFound,String.Format(Messages.NotFound, "Category"), false);
        }

        category.IsDeleted = true;
        _command.CategoryCommand.Update(category);
        await _command.SaveAsync();

        return new DataResponse<string?>(null);
    }
}
