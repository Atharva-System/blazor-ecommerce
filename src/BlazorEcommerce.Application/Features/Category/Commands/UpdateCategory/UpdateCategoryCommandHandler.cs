using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Category;
using Microsoft.AspNetCore.Http;

namespace BlazorEcommerce.Application.Features.Category.Commands.UpdateCategory;

public record UpdateCategoryCommandRequest(CategoryDto category) : IRequest<ServiceResponse<bool>>;

public class DeleteCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, ServiceResponse<bool>>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IMapper _mapper;

    public DeleteCategoryCommandHandler(ICommandUnitOfWork<int> command, IMapper mapper)
    {
        _command = command;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<bool>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Domain.Entities.Category>(request.category);

        if (category == null)
        {
            return new ServiceResponse<bool>() { Success = false, Message = "Category is not found!", StatusCode = StatusCodes.Status404NotFound };
        }

        _command.CategoryCommand.Update(category);
        await _command.SaveAsync();

        return new ServiceResponse<bool>() { Success = true, StatusCode = StatusCodes.Status200OK };
    }
}
