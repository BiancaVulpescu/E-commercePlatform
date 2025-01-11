using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetProductsByTitleQuery : IRequest<ErrorOr<IEnumerable<ProductDtoMinimal>>>
    {
        public required string Title { get; set; }
    }

}
