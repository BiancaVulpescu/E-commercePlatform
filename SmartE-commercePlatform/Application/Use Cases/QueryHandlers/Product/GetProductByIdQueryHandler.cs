using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductDto>>
{
    private readonly IProductRepository repository = repository;
    private readonly IMapper mapper = mapper;

    public async Task<ErrorOr<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetByIdAsync(request.Id, cancellationToken)).Then(mapper.Map<ProductDto>);
    }
}