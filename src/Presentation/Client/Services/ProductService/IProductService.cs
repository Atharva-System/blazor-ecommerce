namespace BlazorEcommerce.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;
        List<ProductDto> Products { get; set; }
        List<ProductDto> AdminProducts { get; set; }
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        Task GetProducts(string? categoryUrl = null);
        Task<ApiResponse<ProductDto>> GetProduct(int productId);
        Task<ProductDto> GetProductDetails(int productId);
        Task SearchProducts(string searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
        Task GetAdminProducts();
        Task<ProductDto> CreateProduct(ProductDto product);
        Task<ProductDto> UpdateProduct(ProductDto product);
        Task DeleteProduct(ProductDto product);
    }
}
