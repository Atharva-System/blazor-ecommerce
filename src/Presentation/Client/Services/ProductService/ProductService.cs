using BlazorEcommerce.Shared.Constant;

namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;
        private const string ProductBaseURL = "api/product/";

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public string Message { get; set; } = "Loading products...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;
        public List<ProductDto> AdminProducts { get; set; }

        public event Action ProductsChanged;

        public async Task<ProductDto> CreateProduct(ProductDto product)
        {
            var result = await _http.PostAsJsonAsync($"{ProductBaseURL}", product);
            var newProductResponse = await result.Content
                .ReadFromJsonAsync<ApiResponse<ProductDto>>();

            if (newProductResponse != null && newProductResponse.Success)
            {
                return newProductResponse.Data;
            }
            return new ProductDto();
        }

        public async Task DeleteProduct(ProductDto product)
        {
            await _http.DeleteAsync($"{ProductBaseURL}{product.Id}");
        }

        public async Task GetAdminProducts()
        {
            var result = await _http
                .GetFromJsonAsync<ApiResponse<List<ProductDto>>>($"{ProductBaseURL}admin");

            if (result != null && result.Success)
            {
                AdminProducts = result.Data;
            }

            CurrentPage = 1;
            PageCount = 0;
            if (AdminProducts.Count == 0)
                Message = "No products found.";
        }

        public async Task<ApiResponse<ProductDto>> GetProduct(int productId)
        {
            return await _http.GetFromJsonAsync<ApiResponse<ProductDto>>($"{ProductBaseURL}{productId}");
        }

        public async Task<ProductDto> GetProductDetails(int productId)
        {
            var result = await _http.GetFromJsonAsync<ApiResponse<ProductDto>>($"{ProductBaseURL}{productId}");
            if (result != null && result.Success)
            {
                return result.Data;
            }
            return null;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
                await _http.GetFromJsonAsync<ApiResponse<List<ProductDto>>>($"{ProductBaseURL}featured") :
                await _http.GetFromJsonAsync<ApiResponse<List<ProductDto>>>($"{ProductBaseURL}category/{categoryUrl}");

            if (result != null && result.Success)
            {
                Products = result.Data;
            }

            CurrentPage = 1;
            PageCount = 0;

            if (Products.Count == 0)
                Message = "No products found";

            ProductsChanged.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _http
                .GetFromJsonAsync<ApiResponse<List<string>>>($"{ProductBaseURL}searchsuggestions/{searchText}");

            if (result != null && result.Success)
            {
                return result.Data;
            }

            return new List<string>();
        }

        public async Task SearchProducts(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _http
                 .GetFromJsonAsync<ApiResponse<ProductSearchResult>>($"{ProductBaseURL}search/{searchText}/{page}");
            if (result != null && result.Success)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            if (Products.Count == 0) Message = String.Format(Messages.NotFound, "Product");

            ProductsChanged?.Invoke();
        }

        public async Task<ProductDto> UpdateProduct(ProductDto product)
        {
            var result = await _http.PutAsJsonAsync($"{ProductBaseURL}", product);
            var content = await result.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();

            if (content != null && content.Success)
            {
                return content.Data;

            }

            return new ProductDto();
        }
    }
}
