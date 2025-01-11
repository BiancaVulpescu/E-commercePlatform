using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetProductsByCategoryQuery : IRequest<ErrorOr<IEnumerable<ProductDtoMinimal>>>
    {
        public Guid CategoryId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

}
