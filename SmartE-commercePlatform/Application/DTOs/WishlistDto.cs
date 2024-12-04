namespace Application.DTOs
{
    public class WishlistDto
    {
        public Guid Id { get; set; }
        public List<ProductDtoMinimal> Products { get; set; } = [];
    }
}