using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetShoppingCartByIdQuery : IRequest<ErrorOr<ShoppingCartDto>>
    {
        public Guid Id { get; set; }
    }
}
