namespace Application.Errors;
    public static class ShoppingCartItemErrors
    {
        public static Error ValidationFailed(string Description) => new Error("ShoppingCartItem.ValidationFailed", Description);

        public static Error NotFound(Guid id)
        {
            return new Error("ShoppingCartItemNotFound", $"Shopping cart item with id {id} not found.");
        }

        public static Error DeleteItemFailed(string message)
        {
            return new Error("DeleteItemFailed", $"Failed to delete shopping cart item. {message}");
        }

        public static Error UpdateItemFailed(string message)
        {
            return new Error("UpdateItemFailed", $"Failed to update shopping cart item. {message}");
        }
        public static Error CreateItemFailed(string message)
        {
            return new Error("CreateItemFailed", $"Failed to create shopping cart item. {message}");
        }
    public static Error GetItemsFailed(string message)
    {
        return new Error("GetItemsFailed", $"Failed to retrieve shopping cart items. {message}");
    }
    public static Error GetItemFailed(string message)
        {
            return new Error("GetItemFailed", $"Failed to retrieve shopping cart item. {message}");
        }
    }
