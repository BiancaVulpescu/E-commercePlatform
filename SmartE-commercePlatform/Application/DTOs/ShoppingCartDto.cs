namespace Application.DTOs
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public List<ProductDtoMinimal> Products { get; set; } = [];
    }
}
