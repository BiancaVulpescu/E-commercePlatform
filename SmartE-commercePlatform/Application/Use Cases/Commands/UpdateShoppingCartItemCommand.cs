﻿using Application.Errors;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateShoppingCartItemCommand : CreateShoppingCartItemBaseCommand, IRequest<Result<Unit, ShoppingCartItemError>>
    {
        public Guid Id { get; set; }
    }
}
