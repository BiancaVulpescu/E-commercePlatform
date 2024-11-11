﻿using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<Unit, ProductError>>
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<Unit, ProductError>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            try
            {
                if  (product is null)
                {
                    return Result<Unit, ProductError>.Err(ProductError.ValidationFailed("The request is null"));
                }
                await repository.UpdateAsync(product);
                return Result<Unit, ProductError>.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit, ProductError>.Err(ProductError.UpdateProductFailed(ex.Message));
            }
        }
    }
}