using Application.Errors;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class DeleteShoppingCartItemCommand : IdCommand, IRequest<Result<Unit, ShoppingCartItemError>>
    {
    }
}
