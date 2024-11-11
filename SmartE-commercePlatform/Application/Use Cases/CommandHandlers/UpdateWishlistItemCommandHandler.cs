using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateWishlistItemCommandHandler : IRequestHandler<UpdateWishlistItemCommand, Result<Unit, WishlistItemError>>
    {
        private readonly IWishlistItemRepository repository;
        private readonly IMapper mapper;

        public UpdateWishlistItemCommandHandler(IWishlistItemRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<Unit, WishlistItemError>> Handle(UpdateWishlistItemCommand request, CancellationToken cancellationToken)
        {
            var wishlistItem = mapper.Map<WishlistItem>(request);
            try
            {
                if (wishlistItem is null)
                {
                    return Result<Unit, WishlistItemError>.Err(WishlistItemError.ValidationFailed("The request is null"));
                }
                await repository.UpdateAsync(wishlistItem);
                return Result<Unit, WishlistItemError>.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit, WishlistItemError>.Err(WishlistItemError.UpdateWishlistItemFailed(ex.Message));
            }
        }
    }
}
