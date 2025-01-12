namespace Application.DTOs
{
    public class OrderProductDtoP
    {
        public ProductDtoMinimal Product { get; set; } = null!;
        public uint Quantity { get; set; }
    }
}
