using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateShoppingCartItemCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
