using BlazorEcommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEcommerce.Domain.Entities;

public class ProductType : BaseAuditableEntity<int>
{
    public string Name { get; set; } = string.Empty;

    [NotMapped]
    public bool Editing { get; set; } = false;
    [NotMapped]
    public bool IsNew { get; set; } = false;
}
