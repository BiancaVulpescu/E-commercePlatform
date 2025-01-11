using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetProductsByCategoryQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetProductsByCategoryQuery, ErrorOr<IEnumerable<ProductDtoMinimal>>>
{
    private readonly IProductRepository repository = repository;
    private readonly IMapper mapper = mapper;
    public async Task<ErrorOr<IEnumerable<ProductDtoMinimal>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetProductsByCategoryAsync(request.CategoryId, cancellationToken))
            .Then(mapper.Map<IEnumerable<ProductDtoMinimal>>);

    }
}
