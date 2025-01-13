namespace Application.Use_Cases.Commands
{
    public class CreateOrderCommandBase
    {
        public Guid TokenId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
