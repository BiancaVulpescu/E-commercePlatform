namespace Domain.Entities 
{
    public class ShoppingCartProduct
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public uint Quantity { get; set; }
    }
}
