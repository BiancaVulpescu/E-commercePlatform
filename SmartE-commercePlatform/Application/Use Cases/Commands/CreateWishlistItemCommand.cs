using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateWishlistItemCommand : CreateWishlistItemBaseCommand, IRequest<Result<Guid>>
    {
    }
}
