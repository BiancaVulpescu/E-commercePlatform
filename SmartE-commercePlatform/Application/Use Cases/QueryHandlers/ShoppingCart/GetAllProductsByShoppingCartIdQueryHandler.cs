using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetAllProductsByShoppingCartIdQueryHandler(IShoppingCartRepository repository, IMapper mapper) : IRequestHandler<GetAllProductsByShoppingCartIdQuery, ErrorOr<IEnumerable<ProductDtoMinimal>>>
{
    private readonly IShoppingCartRepository repository = repository;
    private readonly IMapper mapper = mapper;

    public async Task<ErrorOr<IEnumerable<ProductDtoMinimal>>> Handle(GetAllProductsByShoppingCartIdQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllProductsByShoppingCartIdAsync(request.Id, cancellationToken))
            .Then(mapper.Map<IEnumerable<ProductDtoMinimal>>);
    }
}
