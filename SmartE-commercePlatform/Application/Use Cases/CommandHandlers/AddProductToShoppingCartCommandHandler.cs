using Application.Use_Cases.Commands;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class AddProductToShoppingCartCommandHandler(IShoppingCartRepository repository) : IRequestHandler<AddProductToShoppingCartCommand, ErrorOr<Updated>>
    {
        private readonly IShoppingCartRepository repository = repository;

        public async Task<ErrorOr<Updated>> Handle(AddProductToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            return await repository.AddProductToShoppingCartAsync(request.ShoppingCartId, request.ProductId, cancellationToken);
        }
    }
}
