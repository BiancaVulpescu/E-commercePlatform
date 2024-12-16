using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetProductsByTitleQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetProductsByTitleQuery, ErrorOr<IEnumerable<ProductDtoMinimal>>>
{
    private readonly IProductRepository repository = repository;
    private readonly IMapper mapper = mapper;
    public async Task<ErrorOr<IEnumerable<ProductDtoMinimal>>> Handle(GetProductsByTitleQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetProductsByTitleAsync(request.Title, cancellationToken))
            .Then(mapper.Map<IEnumerable<ProductDtoMinimal>>);

    }
}
