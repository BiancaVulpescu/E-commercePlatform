namespace Application.Use_Cases.Commands
{
    public class CreateShoppingCartItemBaseCommand
    {
        public Guid Product_Id { get; set; }
        public Guid Cart_Id { get; set; }
        public int Quantity { get; set; }
    }
}
