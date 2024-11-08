namespace Domain.Entities 
{
    public class ShoppingCartItems : CartListItemBase
    {
        public Guid Cart_Id { get; set; }
        public int Quantity { get; set; }
    }
}
