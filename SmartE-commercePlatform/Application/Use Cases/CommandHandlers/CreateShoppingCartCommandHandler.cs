using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class CreateShoppingCartCommandHandler(IShoppingCartRepository repository, IMapper mapper) : IRequestHandler<CreateShoppingCartCommand, ErrorOr<Guid>>
    {
        private readonly IShoppingCartRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<Guid>> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            return await repository.AddAsync(mapper.Map<ShoppingCart>(request), cancellationToken);
        }
    }
}
