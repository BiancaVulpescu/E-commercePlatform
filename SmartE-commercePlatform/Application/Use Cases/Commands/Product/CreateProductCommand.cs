using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateProductCommand : CreateProductCommandBase, IRequest<ErrorOr<Guid>>
    {
    }
}
