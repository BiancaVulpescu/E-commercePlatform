using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using Infrastructure.Errors;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WishlistRepository(ApplicationDbContext context) : IWishlistRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task<ErrorOr<IEnumerable<Wishlist>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await context.Wishlists.ToListAsync(cancellationToken);
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Wishlist>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await context.Wishlists
                    .Include(e => e.Products)
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                return entity ?? RepositoryErrors.NotFound.ToErrorOr<Wishlist>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Guid>> AddAsync(Wishlist entity, CancellationToken cancellationToken)
        {
            try
            {
                context.Wishlists.Add(entity);
                await context.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(Wishlist entity, CancellationToken cancellationToken)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync(cancellationToken);
                return Result.Updated;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Deleted>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await context.Wishlists.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                if (entity is not null)
                {
                    context.Remove(entity);
                    await context.SaveChangesAsync(cancellationToken);
                }
                return Result.Deleted;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<IEnumerable<Product>>> GetAllProductsByWishlistIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var wishlist = await context.Wishlists
                    .Include(e => e.Products)
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                return wishlist?.Products ?? RepositoryErrors.NotFound.ToErrorOr<IEnumerable<Product>>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Updated>> AddProductToWishlistAsync(Guid wishlistId, Guid productId, CancellationToken cancellationToken)
        {
            try
            {
                var wishlist = await context.Wishlists
                    .Include(e => e.Products)
                    .FirstOrDefaultAsync(e => e.Id == wishlistId, cancellationToken);
                var product = await context.Products.FirstOrDefaultAsync(e => e.Id == productId, cancellationToken);
                if (product is not null && wishlist is not null)
                {
                    if (wishlist.Products.FirstOrDefault(p => p.Id == product.Id) is null)
                    {
                        wishlist.Products.Add(product);
                        context.Update(wishlist);
                        await context.SaveChangesAsync(cancellationToken);
                    }
                    return Result.Updated;
                }
                return RepositoryErrors.NotFound;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Deleted>> DeleteProductFromWishlistAsync(Guid wishlistId, Guid productId, CancellationToken cancellationToken)
        {
            try
            {
                var wishlist = await context.Wishlists
                    .Include(e => e.Products)
                    .FirstOrDefaultAsync(e => e.Id == wishlistId, cancellationToken);
                var product = await context.Products.FirstOrDefaultAsync(e => e.Id == productId, cancellationToken);
                if (product is not null && wishlist is not null)
                {
                    if (wishlist.Products.Remove(product))
                    {
                        context.Update(wishlist);
                        await context.SaveChangesAsync(cancellationToken);
                    }
                    return Result.Deleted;
                }
                return RepositoryErrors.NotFound;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }
    }
}
