using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ShoppingCartItems>> GetAllItemsAsync(Guid cartId)
        {
            return await context.ShoppingCartItems
                .Where(item => item.Cart_Id == cartId)
                .ToListAsync();
        }

        public async Task<ShoppingCartItems> GetItemByIdAsync(Guid id)
        {
            return await context.ShoppingCartItems.FindAsync(id);
        }

        public async Task<Guid> AddItemAsync(ShoppingCartItems cartItem)
        {
            await context.ShoppingCartItems.AddAsync(cartItem);
            await context.SaveChangesAsync();
            return cartItem.Id; 
        }


        public async Task UpdateItemAsync(ShoppingCartItems cartItem)
        {
            context.Entry(cartItem).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(Guid id)
        {
            var cartItem = await GetItemByIdAsync(id);
            if (cartItem != null)
            {
                context.ShoppingCartItems.Remove(cartItem);
                await context.SaveChangesAsync();
            }
        }
    }
}
