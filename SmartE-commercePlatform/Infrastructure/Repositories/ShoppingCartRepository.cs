using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using Infrastructure.Errors;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ShoppingCartRepository(ApplicationDbContext context) : IShoppingCartRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task<ErrorOr<IEnumerable<ShoppingCart>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await context.ShoppingCarts.ToListAsync(cancellationToken);
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<ShoppingCart>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var shoppingCart = await context.ShoppingCarts
                    .Include(e => e.Products)
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                return shoppingCart ?? RepositoryErrors.NotFound.ToErrorOr<ShoppingCart>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Guid>> AddAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        {
            try
            {
                context.ShoppingCarts.Add(shoppingCart);
                await context.SaveChangesAsync(cancellationToken);
                return shoppingCart.Id;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        {
            try
            {
                context.Entry(shoppingCart).State = EntityState.Modified;
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
                var shoppingCart = await context.ShoppingCarts.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                if (shoppingCart is not null)
                {
                    context.Remove(shoppingCart);
                    await context.SaveChangesAsync(cancellationToken);
                }
                return Result.Deleted;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<IEnumerable<Product>>> GetAllProductsByShoppingCartIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var shoppingCart = await context.ShoppingCarts
                    .Include(e => e.Products)
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                return shoppingCart?.Products ?? RepositoryErrors.NotFound.ToErrorOr<IEnumerable<Product>>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Updated>> AddProductToShoppingCartAsync(Guid shoppingCartId, Guid productId, CancellationToken cancellationToken)
        {
            try
            {
                var shoppingCart = await context.ShoppingCarts
                    .Include(e => e.Products)
                    .FirstOrDefaultAsync(e => e.Id == shoppingCartId, cancellationToken);
                var product = await context.Products.FirstOrDefaultAsync(e => e.Id == productId, cancellationToken);
                if (product is not null && shoppingCart is not null) 
                {
                    if (shoppingCart.Products.FirstOrDefault(p => p.Id == product.Id) is null)
                    {
                        shoppingCart.Products.Add(product);
                        context.Update(shoppingCart);
                        await context.SaveChangesAsync(cancellationToken);
                    }
                    return Result.Updated;
                }
                return RepositoryErrors.NotFound;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Deleted>> DeleteProductFromShoppingCartAsync(Guid shoppingCartId, Guid productId, CancellationToken cancellationToken)
        {
            try
            {
                var shoppingCart = await context.ShoppingCarts
                    .Include(e => e.Products)
                    .FirstOrDefaultAsync(e => e.Id == shoppingCartId, cancellationToken);
                var product = await context.Products.FirstOrDefaultAsync(e => e.Id == productId, cancellationToken);
                if (product is not null && shoppingCart is not null)
                {
                    if (shoppingCart.Products.Remove(product))
                    {
                        context.Update(shoppingCart);
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
