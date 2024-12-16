using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllWishlistsByProductIdQuery : IRequest<ErrorOr<IEnumerable<WishlistDtoMinimal>>>
    {
        public Guid Id { get; set; }
    }
}
