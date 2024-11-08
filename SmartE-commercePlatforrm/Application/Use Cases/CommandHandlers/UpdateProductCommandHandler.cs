using Application.Errors;
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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<Unit>>
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await repository.UpdateAsync(mapper.Map<Product>(request));
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure(ProductErrors.UpdateProductFailed(ex.Message));
            }
        }
    }
}