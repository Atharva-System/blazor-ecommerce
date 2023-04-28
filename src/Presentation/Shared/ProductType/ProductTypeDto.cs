namespace BlazorEcommerce.Shared
{
    public class ProductTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool Editing { get; set; } = false;
        public bool IsNew { get; set; } = false;
    }
}
