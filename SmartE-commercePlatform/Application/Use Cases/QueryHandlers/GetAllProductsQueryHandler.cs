using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandler
{
    public class GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetAllProductsQuery, ErrorOr<IEnumerable<ProductDtoMinimal>>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<IEnumerable<ProductDtoMinimal>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetAllAsync(cancellationToken)).Then(mapper.Map<IEnumerable<ProductDtoMinimal>>);
        }
    }
}
