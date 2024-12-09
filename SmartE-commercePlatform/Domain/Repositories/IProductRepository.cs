using Domain.Entities;
using ErrorOr;

namespace Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<ErrorOr<IEnumerable<ShoppingCart>>> GetAllShoppingCartsByProductIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<IEnumerable<Wishlist>>> GetAllWishlistsByProductIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<IEnumerable<Product>>> GetAllProductsPaginatedAsync(int page, int pageSize, string? title, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken);
        Task<ErrorOr<IEnumerable<Product>>> GetProductsByTitleAsync(string title, CancellationToken cancellationToken);
        
    }
}
