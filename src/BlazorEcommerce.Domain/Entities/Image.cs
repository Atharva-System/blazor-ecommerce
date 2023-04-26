using BlazorEcommerce.Domain.Common;

namespace BlazorEcommerce.Domain.Entities;

public class Image : BaseAuditableEntity<int>
{
    public string Data { get; set; } = string.Empty;
}
