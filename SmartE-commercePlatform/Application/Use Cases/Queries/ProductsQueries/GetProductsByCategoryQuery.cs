using Application.DTOs;
using Application.Errors;
using Application.Utils;
using MediatR;

namespace Application.Use_Cases.Queries.ProductsQueries
{
    public class GetProductsByCategoryQuery : IRequest<Result<List<ProductDto>, ProductError>>
    {
        public string Category { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
