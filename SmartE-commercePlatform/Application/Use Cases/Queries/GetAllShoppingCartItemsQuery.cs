﻿using Application.DTOs;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllShoppingCartItemsQuery : IRequest<Result<List<ShoppingCartItemDto>>>
    {
        public Guid CartId { get; set; }
    }
}