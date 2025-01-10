namespace Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public CategoryDtoMinimal? Category { get; set; }
        public List<ShoppingCartDtoMinimal> ShoppingCarts { get; set; } = [];
        public List<WishlistDtoMinimal> Wishlists { get; set; } = [];
    }
}
