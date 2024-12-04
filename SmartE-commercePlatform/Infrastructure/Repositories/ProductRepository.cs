﻿using Domain.Entities;
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
            {
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
        public async Task<IEnumerable<Product>> GetProductsByTitleAsync(string title)
        {
            return await context.Products.Where(p => p.Title.Contains(title)).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            return await context.Products.Where(p => p.Category.Contains(category)).ToListAsync();
        }

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
