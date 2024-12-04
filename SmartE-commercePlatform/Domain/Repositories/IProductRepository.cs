using Domain.Entities;
using ErrorOr;

namespace Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<ErrorOr<IEnumerable<ShoppingCart>>> GetAllShoppingCartsByProductIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<IEnumerable<Wishlist>>> GetAllWishlistsByProductIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<IEnumerable<Product>>> GetAllProductsPaginatedAsync(int page, CancellationToken cancellationToken);
        Task<ErrorOr<IEnumerable<Product>>> GetProductsByTitleAsync(string title, CancellationToken cancellationToken);
        
    }
}
