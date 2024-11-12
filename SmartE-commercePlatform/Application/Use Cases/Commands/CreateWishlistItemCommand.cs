using Application.Errors;
using Common;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateWishlistItemCommand : CreateWishlistItemBaseCommand, IRequest<Result<Guid, WishlistItemError>>
    {
    }
}
