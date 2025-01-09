using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllShoppingCartsByProductIdQuery : IRequest<ErrorOr<IEnumerable<ShoppingCartDtoMinimal>>>
    {
        public Guid Id { get; set; }
    }
}
