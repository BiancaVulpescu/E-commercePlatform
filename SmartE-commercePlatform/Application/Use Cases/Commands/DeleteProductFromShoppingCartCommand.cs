using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class DeleteProductFromShoppingCartCommand : IRequest<ErrorOr<Deleted>>
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
    }
}
