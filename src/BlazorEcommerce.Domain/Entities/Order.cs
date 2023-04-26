using BlazorEcommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEcommerce.Domain.Entities;

public class Order : BaseAuditableEntity<int>
{
    public string UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
