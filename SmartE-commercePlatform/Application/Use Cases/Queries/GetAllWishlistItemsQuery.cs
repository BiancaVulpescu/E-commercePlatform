using Application.DTOs;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllWishlistItemsQuery : IRequest<List<WishlistItemDto>>
    {
    }
}
