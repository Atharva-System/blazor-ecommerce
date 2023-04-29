namespace BlazorEcommerce.Shared.Cart
{
    public class CartItemDto
    {
        public string? UserId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
