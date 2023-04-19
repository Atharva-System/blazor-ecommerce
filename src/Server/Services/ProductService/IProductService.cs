namespace BlazorEcommerce.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int productId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);
        Task<ServiceResponse<List<Product>>> SearchProductsAsync(string searchText);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestionsAsync(string suggestionText);
        Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
    }
}
