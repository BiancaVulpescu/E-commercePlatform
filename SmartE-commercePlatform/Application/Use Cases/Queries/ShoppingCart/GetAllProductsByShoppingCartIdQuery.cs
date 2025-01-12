using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllProductsByShoppingCartIdQuery : IRequest<ErrorOr<IEnumerable<ShoppingCartProductDtoP>>>
    {
        public Guid Id { get; set; }
    }
}
