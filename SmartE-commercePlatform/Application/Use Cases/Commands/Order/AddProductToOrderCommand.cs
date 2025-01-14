using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class AddProductToOrderCommand : IRequest<ErrorOr<Updated>>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public uint Quantity { get; set; }
    }
}
