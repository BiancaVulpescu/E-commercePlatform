using Application.Use_Cases.Commands;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteProductFromShoppingCartCommandHandler(IShoppingCartRepository repository) : IRequestHandler<DeleteProductFromShoppingCartCommand, ErrorOr<Deleted>>
    {
        private readonly IShoppingCartRepository repository = repository;

        public async Task<ErrorOr<Deleted>> Handle(DeleteProductFromShoppingCartCommand request, CancellationToken cancellationToken)
        {
            return await repository.DeleteProductFromShoppingCartAsync(request.ShoppingCartId, request.ProductId, cancellationToken);
        }
    }
}
