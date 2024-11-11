using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WishlistItemRepository : IWishlistItemRepository
    {
        private readonly ApplicationDbContext context;
        public WishlistItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> AddAsync(WishlistItem wishlistItem)
        {
            await context.WishlistItems.AddAsync(wishlistItem);
            await context.SaveChangesAsync();
            return wishlistItem.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var wishlistItem = await GetByIdAsync(id);
            if (wishlistItem != null)
            {
                context.WishlistItems.Remove(wishlistItem);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<WishlistItem>> GetAllAsync()
        {
            return await context.WishlistItems.ToListAsync();
        }

        public async Task<WishlistItem?> GetByIdAsync(Guid id)
        {
            return await context.WishlistItems.FindAsync(id);
        }

        public async Task UpdateAsync(WishlistItem wishlistItem)
        {
            context.Entry(wishlistItem).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
