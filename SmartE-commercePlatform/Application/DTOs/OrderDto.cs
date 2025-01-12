namespace Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public List<ProductDtoMinimal> Products { get; set; } = [];
    }
}
