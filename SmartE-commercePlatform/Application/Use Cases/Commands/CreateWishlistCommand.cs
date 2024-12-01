using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateWishlistCommand : CreateWishlistCommandBase, IRequest<ErrorOr<Guid>>
    {
    }
}
