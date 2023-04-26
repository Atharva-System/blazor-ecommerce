namespace BlazorEcommerce.Shared.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool Editing { get; set; } = false;
        public bool IsNew { get; set; } = false;
    }
}
