using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using Infrastructure.Errors;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : IProductRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task<ErrorOr<IEnumerable<Product>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {   var products = await context.Products
                    .Include(ICategoryRepository => ICategoryRepository.Category)
                    .ToListAsync(cancellationToken);
                return await context.Products.ToListAsync(cancellationToken);
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Product>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await context.Products
                    .Include(ICategoryRepository => ICategoryRepository.Category)
                    .Include(e => e.ShoppingCarts)
                    .Include(e => e.Wishlists)
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                return product ?? RepositoryErrors.NotFound.ToErrorOr<Product>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Guid>> AddAsync(Product product, CancellationToken cancellationToken)
        {
            try
            {
                context.Products.Add(product);
                await context.SaveChangesAsync(cancellationToken);
                return product.Id;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }
        public async Task<ErrorOr<Updated>> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            try
            {
                context.Entry(product).State = EntityState.Modified;
                await context.SaveChangesAsync(cancellationToken);
                return Result.Updated;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<IEnumerable<Product>>> GetAllProductsPaginatedAsync(int page, int pageSize, string? title, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken)
        {
            try
            {
                var query = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(title))
                {
                    query = query.Where(p => p.Title.Contains(title));
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= maxPrice.Value);
                }
                var products = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);
                return products.Any() ? products : RepositoryErrors.NotFound.ToErrorOr<IEnumerable<Product>>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }
        public async Task<ErrorOr<IEnumerable<Product>>> GetProductsByTitleAsync(string title, CancellationToken cancellationToken)
        {
            try
            {
                var products = await context.Products
                    .Where(e => e.Title == title)
                    .Include(ICategoryRepository => ICategoryRepository.Category)
                    .ToListAsync(cancellationToken);

                return products.Any() ? products : RepositoryErrors.NotFound.ToErrorOr<IEnumerable<Product>>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }
        public async Task<ErrorOr<IEnumerable<Product>>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
        {
            try
            {
                var products = await context.Products
                    .Where(e => e.CategoryId == categoryId)
                    .Include(ICategoryRepository => ICategoryRepository.Category)
                    .ToListAsync(cancellationToken);

                return products.Any() ? products : RepositoryErrors.NotFound.ToErrorOr<IEnumerable<Product>>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }
        //get products by category

        public async Task<ErrorOr<Deleted>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await context.Products.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                if (product is not null)
                {
                    context.Remove(product);
                    await context.SaveChangesAsync(cancellationToken);
                }
                return Result.Deleted;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<IEnumerable<ShoppingCart>>> GetAllShoppingCartsByProductIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await context.Products
                    .Include(e => e.ShoppingCarts)
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                return product?.ShoppingCarts ?? RepositoryErrors.NotFound.ToErrorOr<IEnumerable<ShoppingCart>>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<IEnumerable<Wishlist>>> GetAllWishlistsByProductIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await context.Products
                    .Include(e => e.Wishlists)
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                return product?.Wishlists ?? RepositoryErrors.NotFound.ToErrorOr<IEnumerable<Wishlist>>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }
    }
}
