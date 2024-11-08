using Application.DTOs;
using Application.Use_Cases.Commands;
using MediatR;

namespace Application.Use_Cases.Queries;

public class GetWishlistItemByIdQuery : IdCommand, IRequest<Result<WishlistItemDto>>
{
}