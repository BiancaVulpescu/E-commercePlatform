using Application.Use_Cases.Commands;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteWishlistCommandHandler(IWishlistRepository repository) : IRequestHandler<DeleteWishlistCommand, ErrorOr<Deleted>>
    {
        private readonly IWishlistRepository repository = repository;

        public async Task<ErrorOr<Deleted>> Handle(DeleteWishlistCommand request, CancellationToken cancellationToken)
        {
            return await repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
