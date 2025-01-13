using Application.AIML;
using Domain.Entities;
using Domain.Repositories;

public class ProductDataParser
{
    private readonly Dictionary<Guid, float> productMapping;
    private readonly Dictionary<Guid, float> categoryMapping;

    private readonly IWishlistRepository wishlistRepository;
    private readonly IShoppingCartRepository shoppingCartRepository;

    public ProductDataParser(
        IWishlistRepository wishlistRepository,
        IShoppingCartRepository shoppingCartRepository)
    {
        this.wishlistRepository = wishlistRepository;
        this.shoppingCartRepository = shoppingCartRepository;

        productMapping = new Dictionary<Guid, float>();
        categoryMapping = new Dictionary<Guid, float>();
    }

    public async Task<List<ProductData>> ParseUserProductsAsync(Guid cartsId)
    {
        var wishlistResult = await wishlistRepository.GetByIdAsync(cartsId);
        if (wishlistResult.IsError)
        {
            throw new ArgumentException("Failed to retrieve Wishlists.");
        }

        var shoppingCartResult = await shoppingCartRepository.GetByIdAsync(cartsId);
        if (shoppingCartResult.IsError)
        {
            throw new ArgumentException("Failed to retrieve ShoppingCarts.");
        }

        var wishlists = wishlistResult.Value;
        var shoppingCarts = shoppingCartResult.Value;

        var productsFromWishlists = wishlists.Products.ToList();
        var productsFromShoppingCarts = shoppingCarts.Products.ToList();
        var allProducts = productsFromWishlists.Concat(productsFromShoppingCarts).ToList();

        if (!allProducts.Any())
        {
            throw new ArgumentException("No products found in the user's Wishlists or ShoppingCarts for training.");
        }

        return ParseProducts(allProducts);
    }

    public List<ProductData> ParseProducts(List<Product> products)
    {
        return products.Select(product => new ProductData
        {
            ProductId = GetOrCreateProductId(product.Id),
            CategoryId = product.CategoryId.HasValue ? GetOrCreateCategoryId(product.CategoryId.Value) : 0,
            Price = (float)product.Price
        }).ToList();
    }

    private float GetOrCreateProductId(Guid productId)
    {
        if (!productMapping.ContainsKey(productId))
        {
            productMapping[productId] = productMapping.Count + 1;
        }
        return productMapping[productId];
    }

    private float GetOrCreateCategoryId(Guid categoryId)
    {
        if (!categoryMapping.ContainsKey(categoryId))
        {
            categoryMapping[categoryId] = categoryMapping.Count + 1;
        }
        return categoryMapping[categoryId];
    }
}
