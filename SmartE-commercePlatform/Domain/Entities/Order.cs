namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<Product> Products { get; set; } = [];
        public List<OrderProduct> OrderProducts { get; set; } = [];
    }
}
