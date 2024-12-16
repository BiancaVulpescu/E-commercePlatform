using Application.Use_Cases.Commands;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class AddProductToWishlistCommandHandler(IWishlistRepository repository) : IRequestHandler<AddProductToWishlistCommand, ErrorOr<Updated>>
    {
        private readonly IWishlistRepository repository = repository;

        public async Task<ErrorOr<Updated>> Handle(AddProductToWishlistCommand request, CancellationToken cancellationToken)
        {
            return await repository.AddProductToWishlistAsync(request.WishlistId, request.ProductId, cancellationToken);
        }
    }
}
