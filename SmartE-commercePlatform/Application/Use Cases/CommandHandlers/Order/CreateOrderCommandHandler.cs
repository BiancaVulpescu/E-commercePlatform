using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper) : IRequestHandler<CreateOrderCommand, ErrorOr<Guid>>
    {
        private readonly IOrderRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await repository.AddAsync(mapper.Map<Order>(request), cancellationToken);
        }
    }
}
