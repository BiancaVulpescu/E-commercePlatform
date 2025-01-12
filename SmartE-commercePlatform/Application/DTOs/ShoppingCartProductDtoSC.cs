namespace Application.DTOs
{
    public class ShoppingCartProductDtoSC
    {
        public ShoppingCartDtoMinimal ShoppingCart { get; set; } = null!;
        public uint Quantity { get; set; }
    }
}
