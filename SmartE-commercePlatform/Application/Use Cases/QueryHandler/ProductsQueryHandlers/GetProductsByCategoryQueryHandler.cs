using Application.DTOs;
using Application.Errors;
using Application.Use_Cases.Queries.ProductsQueries;
using Application.Utils;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandler.ProductsQueryHandlers
{
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, Result<List<ProductDto>, ProductError>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByCategoryQueryHandler(IProductRepository _productRepository, IMapper _mapper)
        {
            this._productRepository = _productRepository;
            this._mapper = _mapper;
        }


        public async Task<Result<List<ProductDto>, ProductError>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetProductsByCategoryAsync(request.Category);
            if (result == null)
            {
                return Result<List<ProductDto>, ProductError>.Err(ProductError.FilterNotFound($"There were no products found at this category: {request.Category}"));
            }
            return Result<List<ProductDto>, ProductError>.Ok(_mapper.Map<List<ProductDto>>(result));

        }
    }
}
