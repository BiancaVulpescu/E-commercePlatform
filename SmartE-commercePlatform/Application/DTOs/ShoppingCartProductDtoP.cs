namespace Application.DTOs
{
    public class ShoppingCartProductDtoP
    {
        public ProductDtoMinimal Product { get; set; } = null!;
        public uint Quantity { get; set; }
    }
}
