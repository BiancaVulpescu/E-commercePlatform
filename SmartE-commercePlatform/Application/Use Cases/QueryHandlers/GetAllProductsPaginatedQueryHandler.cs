using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetAllProductsPaginatedQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetAllProductsPaginatedQuery, ErrorOr<IEnumerable<ProductDtoMinimal>>>
{
    private readonly IProductRepository repository;
    private readonly IMapper mapper;
    public async Task<ErrorOr<IEnumerable<ProductDtoMinimal>>> Handle(GetAllProductsPaginatedQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllProductsPaginatedAsync(request.Page, cancellationToken))
            .Then(mapper.Map<IEnumerable<ProductDtoMinimal>>);
    }
}

