using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateProductCommand : IRequest<Result<Guid>>
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsNegociable { get; set; }
    }
}
