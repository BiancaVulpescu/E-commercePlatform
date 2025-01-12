using Domain.Entities;
using ErrorOr;

namespace Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<ErrorOr<IEnumerable<ShoppingCartProduct>>> GetAllShoppingCartsByProductIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ErrorOr<IEnumerable<Wishlist>>> GetAllWishlistsByProductIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ErrorOr<IEnumerable<Product>>> GetAllProductsPaginatedAsync(int page, int pageSize, string? title, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken = default);
        Task<ErrorOr<IEnumerable<Product>>> GetProductsByTitleAsync(string title, CancellationToken cancellationToken = default);
        Task<ErrorOr<IEnumerable<Product>>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        //Task<ErrorOr<IEnumerable<Product>>> GetProductsByCategoryIdAsync(string title, CancellationToken cancellationToken = default); ???

    }
}
