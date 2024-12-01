using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetProductsByTitleAsync(string title);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string title);

        Task<Guid> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
