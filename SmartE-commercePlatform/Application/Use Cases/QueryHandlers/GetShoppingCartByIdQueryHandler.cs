using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandler
{
    public class GetShoppingCartByIdQueryHandler(IShoppingCartRepository repository, IMapper mapper) : IRequestHandler<GetShoppingCartByIdQuery, ErrorOr<ShoppingCartDto>>
    {
        private readonly IShoppingCartRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<ShoppingCartDto>> Handle(GetShoppingCartByIdQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetByIdAsync(request.Id, cancellationToken)).Then(mapper.Map<ShoppingCartDto>);
        }
    }
}
