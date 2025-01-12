namespace Application.DTOs
{
    public class OrderProductDtoO
    {
        public OrderDtoMinimal Order { get; set; } = null!;
        public uint Quantity { get; set; }
    }
}
