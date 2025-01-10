using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

//de implementat
/*public class GetProductsByCategoryQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetProductsByCategoryQuery, ErrorOr<IEnumerable<CategoryDtoMinimal>>>
{
    private readonly IProductRepository repository = repository;
    private readonly IMapper mapper = mapper;
    public async Task<ErrorOr<IEnumerable<ProductDtoMinimal>>> Handle(GetProductsByTitleQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetProductsByTitleAsync(request.Title, cancellationToken))
            .Then(mapper.Map<IEnumerable<ProductDtoMinimal>>);

    }
}*/
