using Domain.Entities;
using ErrorOr;

namespace Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<ErrorOr<IEnumerable<ShoppingCart>>> GetAllShoppingCartsByProductIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<IEnumerable<Wishlist>>> GetAllWishlistsByProductIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetProductsByTitleAsync(string title);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string title);

        Task<Guid> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
