namespace Application.DTOs
{
    public class OrderDtoMinimal
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
