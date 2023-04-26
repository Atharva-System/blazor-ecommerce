using BlazorEcommerce.Shared.Category;

namespace BlazorEcommerce.Application.Features.Category.Commands.AddCategory;

public record AddCategoryCommandRequest(CategoryDto category) : IRequest;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommandRequest>
{
    private readonly ICommandUnitOfWork<int> _command;
    private readonly IMapper _mapper;

    public AddCategoryCommandHandler(ICommandUnitOfWork<int> command, IMapper mapper)
    {
        _command = command;
        _mapper = mapper;
    }

    public async Task Handle(AddCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Domain.Entities.Category>(request.category);
        await _command.CategoryCommand.AddAsync(category);
        await _command.SaveAsync();
    }
}
