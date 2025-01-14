namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public List<Product> Products { get; set; } = [];
        public List<OrderProduct> OrderProducts { get; set; } = [];
    }
}
