using Application.Errors;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class DeleteWishlistItemCommand : IdCommand, IRequest<Result<Unit, WishlistItemError>>
    {
    }
}
