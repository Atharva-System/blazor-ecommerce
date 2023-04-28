using System.Text.Json.Serialization;

namespace BlazorEcommerce.Shared.Product
{
    public class ProductVariantDto
    {
        [JsonIgnore]
        public ProductDto? Product { get; set; }
        public int ProductId { get; set; }
        public ProductTypeDto? ProductType { get; set; }
        public int ProductTypeId { get; set; }

        public decimal Price { get; set; }

        public decimal OriginalPrice { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public bool Editing { get; set; } = false;
        public bool IsNew { get; set; } = false;
    }
}
