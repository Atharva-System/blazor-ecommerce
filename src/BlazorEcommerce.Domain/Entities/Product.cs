using BlazorEcommerce.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Domain.Entities;

public class Product : BaseAuditableEntity<int>
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<Image> Images { get; set; } = new List<Image>();
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    public bool Featured { get; set; } = false;
    public List<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
}
