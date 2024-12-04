namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<ShoppingCart> ShoppingCarts { get; set; } = [];
        public List<ShoppingCartProduct> ShoppingCartProducts { get; set; } = [];
        public List<Wishlist> Wishlists { get; set; } = [];
    }
}