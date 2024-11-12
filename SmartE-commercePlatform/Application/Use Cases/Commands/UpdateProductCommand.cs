﻿using Application.Errors;
using Common;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateProductCommand : CreateProductCommandBase, IRequest<Result<Unit, ProductError>>
    {
        public Guid Id { get; set; }
    }
}
