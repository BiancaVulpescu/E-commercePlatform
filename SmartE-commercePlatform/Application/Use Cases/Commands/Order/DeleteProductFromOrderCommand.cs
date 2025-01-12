using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class DeleteProductFromOrderCommand : IRequest<ErrorOr<Deleted>>
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
    }
}
