namespace Application.Use_Cases.Commands
{
    public class CreateCategoryCommandBase
    {
        public required string Title { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}