using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class AddProductToWishlistCommand : IRequest<ErrorOr<Updated>>
    {
        public Guid WishlistId { get; set; }
        public Guid ProductId { get; set; }
    }
}
