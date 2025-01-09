using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class DeleteWishlistCommand : IRequest<ErrorOr<Deleted>>
    {
        public Guid Id { get; set; }
    }
}
