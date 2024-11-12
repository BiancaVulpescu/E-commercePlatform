using Common;

namespace Application.Errors;
public class ShoppingCartItemError : Error
{
    protected ShoppingCartItemError(string code, string? description = null) : base(code, description) 
    {
    }
    public static ShoppingCartItemError ValidationFailed(string Description) => 
        new ShoppingCartItemError("ShoppingCartItem.ValidationFailed", Description);

    public static ShoppingCartItemError NotFound(Guid id) =>
        new ShoppingCartItemError("ShoppingCartItemNotFound", $"Shopping cart item with id {id} not found.");
    public static ShoppingCartItemError DeleteItemFailed(string message) =>
        new ShoppingCartItemError("DeleteItemFailed", $"Failed to delete shopping cart item. {message}");

    public static ShoppingCartItemError UpdateItemFailed(string message) =>
        new ShoppingCartItemError("UpdateItemFailed", $"Failed to update shopping cart item. {message}");
    public static ShoppingCartItemError CreateItemFailed(string message) =>
        new ShoppingCartItemError("CreateItemFailed", $"Failed to create shopping cart item. {message}");
    public static ShoppingCartItemError GetItemsFailed(string message) =>
        new ShoppingCartItemError("GetItemsFailed", $"Failed to retrieve shopping cart items. {message}");
    public static ShoppingCartItemError GetItemFailed(string message) =>
        new ShoppingCartItemError("GetItemFailed", $"Failed to retrieve shopping cart item. {message}");
}
