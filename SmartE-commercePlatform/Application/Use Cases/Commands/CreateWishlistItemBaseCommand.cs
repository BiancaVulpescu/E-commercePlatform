namespace Application.Use_Cases.Commands
{
    public class CreateWishlistItemBaseCommand
    {
        public Guid Product_Id { get; set; }
        public Guid List_Id { get; set; }
    }
}
