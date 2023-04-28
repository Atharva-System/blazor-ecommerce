using BlazorEcommerce.Domain.Common;

namespace BlazorEcommerce.Domain.Entities;

public class Image : BaseEntity<int>
{
    public string Data { get; set; } = string.Empty;
}
