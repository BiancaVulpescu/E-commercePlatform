using Application.Errors;
using Application.Use_Cases.Commands;
using Domain.Repositories;
using MediatR;
using static Application.Error;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteShoppingCartItemCommandHandler : IRequestHandler<DeleteShoppingCartItemCommand, Result<Unit>>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        public DeleteShoppingCartItemCommandHandler(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Result<Unit>> Handle(DeleteShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = await shoppingCartRepository.GetItemByIdAsync(request.Id);
                if (cartItem is null)
                {
                    return Result<Unit>.Failure(ShoppingCartItemErrors.NotFound(request.Id));
                }
                await shoppingCartRepository.RemoveItemAsync(cartItem.Id);
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception e)
            {
                return Result<Unit>.Failure(ShoppingCartItemErrors.DeleteItemFailed(e.Message));
            }
        }
    }
}
