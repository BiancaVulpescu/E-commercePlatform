using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllProductsByWishlistIdQuery : IRequest<ErrorOr<IEnumerable<ProductDtoMinimal>>>
    {
        public Guid Id { get; set; }
    }
}
