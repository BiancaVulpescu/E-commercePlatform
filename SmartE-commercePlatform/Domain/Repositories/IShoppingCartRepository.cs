using Domain.Entities;
using ErrorOr;

namespace Domain.Repositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart, Guid>
    {
        Task<ErrorOr<IEnumerable<ShoppingCartProduct>>> GetAllProductsByShoppingCartIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ErrorOr<Updated>> AddProductToShoppingCartAsync(Guid shoppingCartId, Guid productId, uint quantity, CancellationToken cancellationToken = default);
        Task<ErrorOr<Deleted>> DeleteProductFromShoppingCartAsync(Guid shoppingCartId, Guid productId, CancellationToken cancellationToken = default);
    }
}
