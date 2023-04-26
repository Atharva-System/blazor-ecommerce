using BlazorEcommerce.Domain.Common;

namespace BlazorEcommerce.Domain.Entities;

public class Category : BaseAuditableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
