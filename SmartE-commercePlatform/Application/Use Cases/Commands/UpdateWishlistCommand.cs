﻿using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateWishlistCommand : CreateWishlistCommandBase, IRequest<ErrorOr<Updated>>
    {
        public Guid Id { get; set; }
    }
}
