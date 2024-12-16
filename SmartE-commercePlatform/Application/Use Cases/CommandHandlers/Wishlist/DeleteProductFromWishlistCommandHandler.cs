using Application.Use_Cases.Commands;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteProductFromWishlistCommandHandler(IWishlistRepository repository) : IRequestHandler<DeleteProductFromWishlistCommand, ErrorOr<Deleted>>
    {
        private readonly IWishlistRepository repository = repository;

        public async Task<ErrorOr<Deleted>> Handle(DeleteProductFromWishlistCommand request, CancellationToken cancellationToken)
        {
            return await repository.DeleteProductFromWishlistAsync(request.WishlistId, request.ProductId, cancellationToken);
        }
    }
}
