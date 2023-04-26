using BlazorEcommerce.Shared.Category;

namespace BlazorEcommerce.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        event Action OnChange;
        List<CategoryDto> Categories { get; set; }
        List<CategoryDto> AdminCategories { get; set; }
        Task GetCategories();
        Task GetAdminCategories();
        Task AddCategory(CategoryDto category);
        Task UpdateCategory(CategoryDto category);
        Task DeleteCategory(int categoryId);
        CategoryDto CreateNewCategory();
    }
}
