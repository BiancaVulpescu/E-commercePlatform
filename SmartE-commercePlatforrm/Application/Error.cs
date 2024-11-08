namespace Application;

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);
    public override string ToString()
    {
        return $"Error {Code} occured. Details: {Description}";
    }
    public static class ShoppingCartErrors
    {
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
        public static Error GetItemsFailed(string message)
        {
            return new Error("GetItemsFailed", $"Failed to retrieve shopping cart items. {message}");
        }

        public static Error GetItemFailed(string message)
        {
            return new Error("GetItemFailed", $"Failed to retrieve shopping cart item. {message}");
        }
    }
}