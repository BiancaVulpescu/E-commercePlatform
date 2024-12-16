namespace Application.Use_Cases.Commands
{
    public class CreateProductCommandBase
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}