using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateWishlistItemCommandHandler : IRequestHandler<UpdateWishlistItemCommand, Result<Unit>>
    {
        private readonly IWishlistItemRepository repository;
        private readonly IMapper mapper;

        public UpdateWishlistItemCommandHandler(IWishlistItemRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(UpdateWishlistItemCommand request, CancellationToken cancellationToken)
        {
            var wishlistItem = mapper.Map<WishlistItem>(request);
            try
            {
                if (wishlistItem is null)
                {
                    return Result<Unit>.Failure(WishlistItemErrors.ValidationFailed("The request is null"));
                }
                await repository.UpdateAsync(wishlistItem);
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure(WishlistItemErrors.UpdateWishlistItemFailed(ex.Message));
            }
        }
    }
}
