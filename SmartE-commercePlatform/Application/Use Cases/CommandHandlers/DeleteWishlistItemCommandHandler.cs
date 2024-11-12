using Application.Errors;
using Application.Use_Cases.Commands;
using Common;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteWishlistItemCommandHandler : IRequestHandler<DeleteWishlistItemCommand, Result<Unit, WishlistItemError>>
    {
        private readonly IWishlistItemRepository wishlistItemRepository;
        public DeleteWishlistItemCommandHandler(IWishlistItemRepository wishlistItemRepository)
        {
            this.wishlistItemRepository = wishlistItemRepository;
        }
        public async Task<Result<Unit, WishlistItemError>> Handle(DeleteWishlistItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var wishlistItem = await wishlistItemRepository.GetByIdAsync(request.Id);
                if (wishlistItem is null)
                {
                    return Result<Unit, WishlistItemError>.Err(WishlistItemError.NotFound(request.Id));
                }
                await wishlistItemRepository.DeleteAsync(wishlistItem.Id);
                return Result<Unit, WishlistItemError>.Ok(Unit.Value);
            }
            catch (Exception e)
            {
                return Result<Unit, WishlistItemError>.Err(WishlistItemError.DeleteWishlistItemFailed(e.Message));
            }
        }
    }
}
