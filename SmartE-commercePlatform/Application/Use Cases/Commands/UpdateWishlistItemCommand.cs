using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateWishlistItemCommand : CreateWishlistItemBaseCommand, IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
