namespace Application.DTOs

{
    public class ShoppingCartItemsDto : CartListItemsBaseDto
    {
        public Guid Cart_Id { get; set; }
        public int Quantity { get; set; }
    }
}
