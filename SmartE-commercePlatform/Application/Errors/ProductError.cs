namespace Application.Errors;

public class ProductError : Error
{
    protected ProductError(string code, string? description = null) : base(code, description) 
    {
    }
    public static ProductError ValidationFailed(string Description) => new ProductError("Product.ValidationFailed", Description);
    public static ProductError NotFound(Guid guid) =>
        new ProductError("Product.NotFound", $"The product with id: {guid} was not found.");
   
    public static ProductError ProductExists(Guid guid) =>
        new ProductError("Product.ProductExists", $"The product with id: {guid} already exists.");
   
    public static ProductError CreateProductFailed(string Description) =>
        new ProductError("Product.CreateProductFailed", Description);
    
    public static ProductError GetProductFailed(string Description) =>
        new ProductError("Product.GetProductFailed", Description);
    
    public static ProductError DeleteProductFailed(string Description) =>
        new ProductError("Product.DeleteProductFailed", Description);
    
    public static ProductError UpdateProductFailed(string Description) =>
        new ProductError("Product.UpdateProductFailed", Description);
}