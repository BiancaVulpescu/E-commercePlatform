using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllProductsByShoppingCartIdQuery : IRequest<ErrorOr<IEnumerable<ProductDtoMinimal>>>
    {
        public Guid Id { get; set; }
    }
}
