namespace Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Status { get; set; } 
        public List<ProductDtoMinimal> Products { get; set; } = [];
    }
}
