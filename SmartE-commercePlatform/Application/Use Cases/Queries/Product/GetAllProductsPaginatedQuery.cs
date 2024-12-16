using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllProductsPaginatedQuery : IRequest<ErrorOr<IEnumerable<ProductDtoMinimal>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Title { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    
}
