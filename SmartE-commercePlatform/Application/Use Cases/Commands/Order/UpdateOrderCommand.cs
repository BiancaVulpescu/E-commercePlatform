using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateOrderCommand : CreateOrderCommandBase, IRequest<ErrorOr<Updated>>
    {
        public Guid Id { get; set; }
    }
}
