using Domain.Entities;

namespace Domain.Repositories
{
    public interface IWishlistItemRepository
    {
        Task<IEnumerable<WishlistItem>> GetAllAsync();
        Task<WishlistItem?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(WishlistItem wishlistItem);
        Task UpdateAsync(WishlistItem wishlistItem);
        Task DeleteAsync(Guid id);
    }
}
