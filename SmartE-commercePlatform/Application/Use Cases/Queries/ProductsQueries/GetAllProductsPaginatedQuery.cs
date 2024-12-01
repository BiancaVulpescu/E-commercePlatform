using Application.DTOs;
using Application.Utils;
using MediatR;

namespace Application.Use_Cases.Queries.ProductsQueries
{
    public class GetAllProductsPaginatedQuery : IRequest<Result<PagedResult<ProductDto>, string>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    
}
