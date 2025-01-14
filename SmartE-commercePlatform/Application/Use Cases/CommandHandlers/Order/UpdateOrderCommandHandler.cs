﻿using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateOrderCommandHandler(IOrderRepository repository, IMapper mapper) : IRequestHandler<UpdateOrderCommand, ErrorOr<Updated>>
    {
        private readonly IOrderRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<Updated>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            return await repository.UpdateAsync(mapper.Map<Order>(request), cancellationToken);
        }
    }
}
