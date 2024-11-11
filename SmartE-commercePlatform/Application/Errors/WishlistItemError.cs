namespace Application.Errors;

public class WishlistItemError : Error
{
    protected WishlistItemError(string code, string? description = null) : base(code, description)
    {
    }
    public static WishlistItemError ValidationFailed(string Description) => 
        new WishlistItemError("WishlistItem.ValidationFailed", Description);

    public static WishlistItemError NotFound(Guid guid) =>
        new WishlistItemError("WishlistItem.NotFound", $"The wishlist item with id: {guid} was not found.");

    public static WishlistItemError WishlistItemExists(Guid guid) =>
        new WishlistItemError("WishlistItem.WishlistItemExists", $"The wishlist item with id: {guid} already exists.");

    public static WishlistItemError CreateWishlistItemFailed(string description) =>
        new WishlistItemError("WishlistItem.CreateWishlistItemFailed", description);

    public static WishlistItemError GetWishlistItemFailed(string description) =>
        new WishlistItemError("WishlistItem.GetWishlistItemFailed", description);

    public static WishlistItemError DeleteWishlistItemFailed(string description) =>
        new WishlistItemError("WishlistItem.DeleteWishlistItemFailed", description);

    public static WishlistItemError UpdateWishlistItemFailed(string description) =>
        new WishlistItemError("WishlistItem.UpdateWishlistItemFailed", description);
}
