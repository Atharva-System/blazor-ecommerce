namespace BlazorEcommerce.Shared.Product
{
    public class ProductSearchResult
    {
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
