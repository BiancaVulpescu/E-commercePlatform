namespace Domain.Entities
{
    public class CartListItemBase
    {
        public Guid Id { get; set; }
        public Guid Product_Id { get; set; }
        public virtual Product? Product { get; set; }
    }
}
