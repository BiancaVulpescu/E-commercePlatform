using Application.Errors;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateShoppingCartItemCommand : CreateShoppingCartItemBaseCommand, IRequest<Result<Guid, ShoppingCartItemError>>
    {
    }

}
