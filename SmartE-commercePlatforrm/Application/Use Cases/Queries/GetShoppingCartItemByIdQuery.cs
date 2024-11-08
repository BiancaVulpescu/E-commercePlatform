using Application.DTOs;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetShoppingCartItemByIdQuery : IRequest<Result<ShoppingCartItemsDto>>
    {
        public Guid Id { get; set; }

    }
}
