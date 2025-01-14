namespace Domain.Entities
{
    public class OrderProduct
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public Product Product { get; set; } = null!;
        public Order Order { get; set; } = null!;
        public uint Quantity;
    }
}
