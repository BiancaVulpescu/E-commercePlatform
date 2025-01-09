namespace Application.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
