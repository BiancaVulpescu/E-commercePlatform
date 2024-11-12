using Application.Errors;
using Common;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class DeleteShoppingCartItemCommand : IdCommand, IRequest<Result<Unit, ShoppingCartItemError>>
    {
    }
}
