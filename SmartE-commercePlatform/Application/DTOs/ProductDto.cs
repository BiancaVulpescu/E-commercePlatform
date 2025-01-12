namespace Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public CategoryDtoMinimal? Category { get; set; }
        public List<ShoppingCartProductDtoSC> ShoppingCartProducts { get; set; } = [];
        public List<WishlistDtoMinimal> Wishlists { get; set; } = [];
        public List<OrderProductDtoO> OrderProducts { get; set; } = [];
    }
}
