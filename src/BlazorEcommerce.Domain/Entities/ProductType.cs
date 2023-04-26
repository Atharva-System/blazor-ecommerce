using BlazorEcommerce.Domain.Common;

namespace BlazorEcommerce.Domain.Entities;

public class ProductType : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;

}
