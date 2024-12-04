namespace Domain.Entities
{
    public class Wishlist
    {
        public Guid Id { get; set; }
        public List<Product> Products { get; set; } = [];
    }
}