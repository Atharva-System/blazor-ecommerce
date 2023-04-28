namespace BlazorEcommerce.Client.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        event Action OnChange;
        public List<ProductTypeDto> ProductTypes { get; set; }
        Task GetProductTypes();
        Task AddProductType(ProductTypeDto productType);
        Task UpdateProductType(ProductTypeDto productType);
        ProductTypeDto CreateNewProductType();
    }
}
