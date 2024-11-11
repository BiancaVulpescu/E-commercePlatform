namespace Application.Use_Cases.Commands
{
    public class CreateProductCommandBase
    {
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsNegotiable { get; set; }
    }
}