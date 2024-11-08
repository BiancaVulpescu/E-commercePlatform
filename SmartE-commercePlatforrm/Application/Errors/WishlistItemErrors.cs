namespace Application.Errors;

public static class WishlistItemsErrors
{
    public static Error NotFound(Guid guid) =>
        new Error("WishlistItem.NotFound", $"The wishlist item with id: {guid} was not found.");

    public static Error WishlistItemExists(Guid guid) =>
        new Error("WishlistItem.WishlistItemExists", $"The wishlist item with id: {guid} already exists.");

    public static Error CreateWishlistItemFailed(string description) =>
        new Error("WishlistItem.CreateWishlistItemFailed", description);

    public static Error GetWishlistItemFailed(string description) =>
        new Error("WishlistItem.GetWishlistItemFailed", description);

    public static Error DeleteWishlistItemFailed(string description) =>
        new Error("WishlistItem.DeleteWishlistItemFailed", description);

    public static Error UpdateWishlistItemFailed(string description) =>
        new Error("WishlistItem.UpdateWishlistItemFailed", description);
}
