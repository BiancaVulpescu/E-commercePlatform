using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateOrderCommand : CreateOrderCommandBase, IRequest<ErrorOr<Guid>>
    {
    }
}
