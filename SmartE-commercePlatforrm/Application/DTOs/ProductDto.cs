namespace Application.DTOs
{
    internal class ProductDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsNegociable { get; set; }
        public string Location { get; set; }
    }
}
