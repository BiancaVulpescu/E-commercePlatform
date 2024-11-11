﻿using Application.DTOs;
using Application.Errors;
using Application.Use_Cases.Commands;
using MediatR;

namespace Application.Use_Cases.Queries;

public class GetProductByIdQuery : IdCommand, IRequest<Result<ProductDto, ProductError>>
{
}