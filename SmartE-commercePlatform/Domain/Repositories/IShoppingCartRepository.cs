using Domain.Entities;
using ErrorOr;

namespace Domain.Repositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart, Guid>
    {
        Task<ErrorOr<IEnumerable<Product>>> GetAllProductsByShoppingCartIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<Updated>> AddProductToShoppingCartAsync(Guid shoppingCartId, Guid productId, CancellationToken cancellationToken);
        Task<ErrorOr<Deleted>> DeleteProductFromShoppingCartAsync(Guid shoppingCartId, Guid productId, CancellationToken cancellationToken);
    }
}
