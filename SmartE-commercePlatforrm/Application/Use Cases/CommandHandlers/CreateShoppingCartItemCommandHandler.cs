﻿using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.CommandHandlers
{
    internal class CreateShoppingCartItemCommandHandler : IRequestHandler<CreateShoppingCartItemCommand, Result<Guid>>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper _mapper;

        public CreateShoppingCartItemCommandHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = _mapper.Map<ShoppingCartItems>(request);

            try
            {
                var returnedId = await repository.AddItemAsync(cartItem);
                return Result<Guid>.Success(returnedId);
            }
            catch (Exception e)
            {
                return Result<Guid>.Failure(ProductErrors.CreateProductFailed(e.Message)); // You can adjust the error handling to be specific to cart items
            }
        }
    }
}