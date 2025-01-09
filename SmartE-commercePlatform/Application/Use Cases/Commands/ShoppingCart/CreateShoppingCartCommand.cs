using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateShoppingCartCommand : CreateShoppingCartCommandBase, IRequest<ErrorOr<Guid>>
    {
    }

}
