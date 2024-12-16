﻿using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllWishlistsQuery : IRequest<ErrorOr<IEnumerable<WishlistDtoMinimal>>>
    {
    }
}