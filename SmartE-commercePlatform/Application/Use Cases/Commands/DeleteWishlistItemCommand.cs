using Application.Errors;
using Common;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class DeleteWishlistItemCommand : IdCommand, IRequest<Result<Unit, WishlistItemError>>
    {
    }
}
