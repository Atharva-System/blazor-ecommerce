using BlazorEcommerce.Shared.Category;

namespace BlazorEcommerce.Application.MappingProfıles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}
