using Application.DTOs;
using Application.Use_Cases.Queries.ProductsQueries;
using Application.Utils;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandler.ProductsQueryHandlers
{
    public class GetAllProductsPaginatedQueryHandler : IRequestHandler<GetAllProductsPaginatedQuery, Result<PagedResult<ProductDto>, string>>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository repository;

        public GetAllProductsPaginatedQueryHandler(IMapper mapper, IProductRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<Result<PagedResult<ProductDto>, string>> Handle (GetAllProductsPaginatedQuery query, CancellationToken cancellationToken)
        {
            var products = await repository.GetAllAsync();
            if(products == null)
            {
                return Result<PagedResult<ProductDto>, string>.Err("No products found");
            }
            var queryableProducts = products.AsQueryable();
            var pagedProducts = queryableProducts
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();
            var productDtos = mapper.Map<List<ProductDto>>(pagedProducts);
            var pagedResult = new PagedResult<ProductDto>(productDtos, queryableProducts.Count());
            return Result<PagedResult<ProductDto>, string>.Ok(pagedResult);
        }
    }
   
}
