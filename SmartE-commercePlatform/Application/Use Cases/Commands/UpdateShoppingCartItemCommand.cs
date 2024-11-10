using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateShoppingCartItemCommand : CreateShoppingCartItemBaseCommand, IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
