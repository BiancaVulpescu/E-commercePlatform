using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetWishlistByIdQuery : IRequest<ErrorOr<WishlistDto>>
    {
        public Guid Id { get; set; }
    }
}