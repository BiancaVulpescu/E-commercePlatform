using Application.Errors;
using Application.Use_Cases.Commands;
using Common;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteShoppingCartItemCommandHandler : IRequestHandler<DeleteShoppingCartItemCommand, Result<Unit, ShoppingCartItemError>>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        public DeleteShoppingCartItemCommandHandler(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Result<Unit, ShoppingCartItemError>> Handle(DeleteShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = await shoppingCartRepository.GetItemByIdAsync(request.Id);
                if (cartItem is null)
                {
                    return Result<Unit, ShoppingCartItemError>.Err(ShoppingCartItemError.NotFound(request.Id));
                }
                await shoppingCartRepository.RemoveItemAsync(cartItem.Id);
                return Result<Unit, ShoppingCartItemError>.Ok(Unit.Value);
            }
            catch (Exception e)
            {
                return Result<Unit, ShoppingCartItemError>.Err(ShoppingCartItemError.DeleteItemFailed(e.Message));
            }
        }
    }
}
