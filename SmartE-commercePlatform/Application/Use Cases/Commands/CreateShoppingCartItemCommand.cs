using Application.Errors;
using Common;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateShoppingCartItemCommand : CreateShoppingCartItemBaseCommand, IRequest<Result<Guid, ShoppingCartItemError>>
    {
    }

}
