namespace BlazorEcommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _dataContext.Products
                            .Include(x => x.Variants)
                            .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _dataContext.Products
                .Include(x => x.Variants)
                .ThenInclude(x => x.ProductType)
                .FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this product does not exists.";
            }
            else
            {
                response.Data = product;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _dataContext.Products
                                .Where(x => x.Category != null && x.Category.Url.ToLower() == categoryUrl.ToLower())
                                .Include(x => x.Variants)
                                .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> SearchProductsAsync(string searchText)
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await FindProductBySearchText(searchText)
            };

            return response;
        }

        private Task<List<Product>> FindProductBySearchText(string searchText)
        {
            return _dataContext.Products
                                       .Where(t => t.Title.ToLower().Contains(searchText.ToLower())
                                           ||
                                           t.Description.ToLower().Contains(searchText.ToLower()))
                                       .Include(x => x.Variants)
                                       .ToListAsync();
        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestionsAsync(string suggestionText)
        {
            var products = await FindProductBySearchText(suggestionText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(suggestionText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuations = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();

                    var words = product.Description.Split().Select(s => s.Trim(punctuations));

                    foreach (var word in words)
                    {
                        if (word.Contains(suggestionText, StringComparison.OrdinalIgnoreCase)
                            && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };

        }
    }
}
