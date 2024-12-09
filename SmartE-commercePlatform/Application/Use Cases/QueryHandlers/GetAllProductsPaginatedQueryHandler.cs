using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetAllProductsPaginatedQueryHandler: IRequestHandler<GetAllProductsPaginatedQuery, ErrorOr<IEnumerable<ProductDtoMinimal>>>
{
    private readonly IProductRepository repository;
    private readonly IMapper mapper;
    public GetAllProductsPaginatedQueryHandler(IProductRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    public async Task<ErrorOr<IEnumerable<ProductDtoMinimal>>> Handle(GetAllProductsPaginatedQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllProductsPaginatedAsync(request.Page, request.PageSize, request.Title, request.MinPrice, request.MaxPrice, cancellationToken))
            .Then(mapper.Map<IEnumerable<ProductDtoMinimal>>);
    }
}

