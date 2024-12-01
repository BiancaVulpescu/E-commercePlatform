using Application.DTOs;
using Application.Errors;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetShoppingCartItemByIdQuery : IRequest<Result<ShoppingCartItemDto, ShoppingCartItemError>>
    {
        public Guid Id { get; set; }

    }
}
