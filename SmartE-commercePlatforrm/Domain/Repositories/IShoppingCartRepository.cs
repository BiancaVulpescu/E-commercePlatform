using Domain.Entities;

namespace Domain.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<ShoppingCartItems>> GetAllItemsAsync();
        Task<ShoppingCartItems> GetItemByIdAsync(Guid id);
        Task<Guid> AddItemAsync(ShoppingCartItems cartItem);
        Task UpdateItemAsync(ShoppingCartItems cartItem);
        Task RemoveItemAsync(Guid id);
    }
}
