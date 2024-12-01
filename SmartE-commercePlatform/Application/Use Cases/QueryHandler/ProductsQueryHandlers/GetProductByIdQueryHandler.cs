using Application.DTOs;
using Application.Errors;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto, ProductError>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProductDto, ProductError>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            return product is null
                ? Result<ProductDto, ProductError>.Err(ProductError.NotFound(request.Id))
                : Result<ProductDto, ProductError>.Ok(_mapper.Map<ProductDto>(product));
        }
        catch (Exception e)
        {
            return Result<ProductDto, ProductError>.Err(ProductError.GetProductFailed(e.Message));
        }
    }
}