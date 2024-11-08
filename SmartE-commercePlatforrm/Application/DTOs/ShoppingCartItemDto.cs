namespace Application.DTOs

{
    public class ShoppingCartItemsDto : CartListItemBaseDto
    {
        public Guid Cart_Id { get; set; }
        public int Quantity { get; set; }
    }
}
