namespace Application.DTOs
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public List<ShoppingCartProductDtoP> ShoppingCartProducts { get; set; } = [];
    }
}
