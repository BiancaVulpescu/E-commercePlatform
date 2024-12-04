﻿using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateShoppingCartCommandHandler(IShoppingCartRepository repository, IMapper mapper) : IRequestHandler<UpdateShoppingCartCommand, ErrorOr<Updated>>
    {
        private readonly IShoppingCartRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<Updated>> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            return await repository.UpdateAsync(mapper.Map<ShoppingCart>(request), cancellationToken);
        }
    }
}