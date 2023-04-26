using BlazorEcommerce.Domain.Common;

namespace BlazorEcommerce.Domain.Entities
{
    public class CartItem : BaseEntity<int>
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
