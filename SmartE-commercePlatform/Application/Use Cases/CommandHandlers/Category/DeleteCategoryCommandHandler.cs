﻿using Application.Use_Cases.Commands;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteCategoryCommandHandler(IProductRepository repository) : IRequestHandler<DeleteCategoryCommand, ErrorOr<Deleted>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<ErrorOr<Deleted>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return await repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
