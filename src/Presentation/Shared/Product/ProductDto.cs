using BlazorEcommerce.Shared.Category;
using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Shared.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<ImageDto> Images { get; set; } = new List<ImageDto>();
        public CategoryDto? Category { get; set; }
        public int CategoryId { get; set; }
        public bool Featured { get; set; } = false;
        public List<ProductVariantDto> Variants { get; set; } = new List<ProductVariantDto>();
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public bool Editing { get; set; } = false;
        public bool IsNew { get; set; } = false;
    }
}
