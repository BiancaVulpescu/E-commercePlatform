using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class CreateWishlistItemCommandHandler : IRequestHandler<CreateWishlistItemCommand, Result<Guid, WishlistItemError>>
    {
        private readonly IWishlistItemRepository repository;
        private readonly IMapper _mapper;

        public CreateWishlistItemCommandHandler(IWishlistItemRepository repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<Guid, WishlistItemError>> Handle(CreateWishlistItemCommand request, CancellationToken cancellationToken)
        {
            var wishlistItem = _mapper.Map<WishlistItem>(request);
            if (wishlistItem is null)
            {
                return Result<Guid, WishlistItemError>.Err(WishlistItemError.ValidationFailed("The wishlist item is null"));
            }
            try
            {
                var returnedId = await repository.AddAsync(wishlistItem);
                return Result<Guid, WishlistItemError>.Ok(returnedId);
            }
            catch (Exception e)
            {
                return Result<Guid, WishlistItemError>.Err(WishlistItemError.CreateWishlistItemFailed(e.Message));
            }
        }
    }
}
