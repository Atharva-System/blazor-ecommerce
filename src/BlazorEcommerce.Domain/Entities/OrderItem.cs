using BlazorEcommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEcommerce.Domain.Entities;

public class OrderItem : BaseEntity<int>
{
    public Order Order { get; set; }
    public int OrderId { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public ProductType ProductType { get; set; }
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }
}
