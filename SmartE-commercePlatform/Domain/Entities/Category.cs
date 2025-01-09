namespace Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category> Subcategories { get; set; } = [];
    }
}