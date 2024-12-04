using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetAllShoppingCartsByProductIdQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetAllShoppingCartsByProductIdQuery, ErrorOr<IEnumerable<ShoppingCartDtoMinimal>>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<IEnumerable<ShoppingCartDtoMinimal>>> Handle(GetAllShoppingCartsByProductIdQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetAllShoppingCartsByProductIdAsync(request.Id, cancellationToken))
                .Then(mapper.Map<IEnumerable<ShoppingCartDtoMinimal>>);
        }
    }
}
