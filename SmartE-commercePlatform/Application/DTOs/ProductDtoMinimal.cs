namespace Application.DTOs
{
    public class ProductDtoMinimal
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
