namespace Domain.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public List<Product> Products { get; set; } = [];
        public List<ShoppingCartProduct> ShoppingCartProducts { get; set;} = [];
    }
}
