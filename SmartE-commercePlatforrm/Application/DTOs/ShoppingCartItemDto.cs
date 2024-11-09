namespace Application.DTOs

{
    public class ShoppingCartItemDto : CartListItemBaseDto
    {
        public Guid Cart_Id { get; set; }
        public int Quantity { get; set; }
    }
}
