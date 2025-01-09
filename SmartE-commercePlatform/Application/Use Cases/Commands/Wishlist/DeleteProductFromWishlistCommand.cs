using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class DeleteProductFromWishlistCommand : IRequest<ErrorOr<Deleted>>
    {
        public Guid WishlistId { get; set; }
        public Guid ProductId { get; set; }
    }
}
