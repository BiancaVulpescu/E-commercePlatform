namespace Domain.Entities 
{
    public class ShoppingCartProduct
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
        //TODO
        //public uint Quantity { get; set; }
    }
}
