using Application.Errors;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateWishlistItemCommand : CreateWishlistItemBaseCommand, IRequest<Result<Unit, WishlistItemError>>
    {
        public Guid Id { get; set; }
    }
}
