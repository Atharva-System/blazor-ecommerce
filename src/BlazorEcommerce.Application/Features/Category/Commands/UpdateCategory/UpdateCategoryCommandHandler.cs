using BlazorEcommerce.Shared.Category;
using Microsoft.AspNetCore.Http;

namespace BlazorEcommerce.Application.Features.Category.Commands.UpdateCategory;

public record UpdateCategoryCommandRequest(CategoryDto category) : IRequest<IResponse>;

public class DeleteCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, IResponse>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IMapper _mapper;
    private readonly IQueryUnitOfWork _query;

    public DeleteCategoryCommandHandler(ICommandUnitOfWork<int> command, IMapper mapper, IQueryUnitOfWork query)
    {
        _command = command;
        _mapper = mapper;
        _query = query;
    }

    public async Task<IResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var category = await _query.CategoryQuery.GetByIdAsync(cat => cat.Id == request.category.Id);
        if (category == null)
        {
            return new DataResponse<string?>(null, HttpStatusCodes.NotFound, String.Format(Messages.NotFound, "Category"), false);
        }

        category = _mapper.Map<Domain.Entities.Category>(request.category);

        if (category == null)
        {
            return new DataResponse<string?>(null, HttpStatusCodes.NotFound, String.Format(Messages.NotFound, "Category"), false);
        }

        _command.CategoryCommand.Update(category);
        await _command.SaveAsync();

        return new DataResponse<string?>(null);
    }
}
