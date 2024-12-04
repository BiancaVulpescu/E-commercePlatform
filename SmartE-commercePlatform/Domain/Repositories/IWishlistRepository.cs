using Domain.Entities;
using ErrorOr;

namespace Domain.Repositories
{
    public interface IWishlistRepository : IRepository<Wishlist, Guid>
    {
        Task<ErrorOr<IEnumerable<Product>>> GetAllProductsByWishlistIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<Updated>> AddProductToWishlistAsync(Guid wishlistId, Guid productId, CancellationToken cancellationToken);
        Task<ErrorOr<Deleted>> DeleteProductFromWishlistAsync(Guid wishlistId, Guid productId, CancellationToken cancellationToken);
    }
}
