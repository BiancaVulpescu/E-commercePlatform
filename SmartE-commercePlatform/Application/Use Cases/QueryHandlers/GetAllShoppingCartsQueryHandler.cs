using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetAllShoppingCartsQueryHandler(IShoppingCartRepository repository, IMapper mapper) : IRequestHandler<GetAllShoppingCartsQuery, ErrorOr<IEnumerable<ShoppingCartDtoMinimal>>>
{
    private readonly IShoppingCartRepository repository = repository;
    private readonly IMapper mapper = mapper;

    public async Task<ErrorOr<IEnumerable<ShoppingCartDtoMinimal>>> Handle(GetAllShoppingCartsQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllAsync(cancellationToken))
            .Then(mapper.Map<IEnumerable<ShoppingCartDtoMinimal>>);
    }
}
