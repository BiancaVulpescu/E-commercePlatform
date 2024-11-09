using Application.DTOs;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetShoppingCartItemByIdQuery : IRequest<Result<ShoppingCartItemDto>>
    {
        public Guid Id { get; set; }

    }
}
