using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class AddProductToShoppingCartCommand : IRequest<ErrorOr<Updated>>
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
        public uint Quantity { get; set; }
    }
}
