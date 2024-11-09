using Domain.Entities;

namespace Domain.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<ShoppingCartItem>> GetAllItemsAsync();
        Task<ShoppingCartItem> GetItemByIdAsync(Guid id);
        Task<Guid> AddItemAsync(ShoppingCartItem cartItem);
        Task UpdateItemAsync(ShoppingCartItem cartItem);
        Task RemoveItemAsync(Guid id);
    }
}
