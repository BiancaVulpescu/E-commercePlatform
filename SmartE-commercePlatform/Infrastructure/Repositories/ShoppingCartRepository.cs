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

        public async Task<IEnumerable<ShoppingCartItem>> GetAllItemsAsync()
        {
            return await context.ShoppingCartItem.ToListAsync();
        }

        public async Task<ShoppingCartItem> GetItemByIdAsync(Guid id)
        {
            return await context.ShoppingCartItem.FindAsync(id);
        }

        public async Task<Guid> AddItemAsync(ShoppingCartItem cartItem)
        {
            await context.ShoppingCartItem.AddAsync(cartItem);
            await context.SaveChangesAsync();
            return cartItem.Id; 
        }


        public async Task UpdateItemAsync(ShoppingCartItem cartItem)
        {
            context.Entry(cartItem).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(Guid id)
        {
            var cartItem = await GetItemByIdAsync(id);
            if (cartItem != null)
            {
                context.ShoppingCartItem.Remove(cartItem);
                await context.SaveChangesAsync();
            }
        }
    }
}
