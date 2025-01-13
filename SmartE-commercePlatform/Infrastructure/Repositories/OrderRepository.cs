using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using Infrastructure.Errors;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : IOrderRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task<ErrorOr<IEnumerable<Order>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await context.Orders.ToListAsync(cancellationToken);
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }
        public async Task<ErrorOr<IEnumerable<Order>>> GetAllOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                return await context.Orders
                    .Where(e => e.UserId == userId)
                    .ToListAsync(cancellationToken);
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Order>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var order = await context.Orders
                    .Include(e => e.Products)
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                return order ?? RepositoryErrors.NotFound.ToErrorOr<Order>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Guid>> AddAsync(Order order, CancellationToken cancellationToken)
        {
            try
            {
                context.Orders.Add(order);
                await context.SaveChangesAsync(cancellationToken);
                return order.Id;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(Order order, CancellationToken cancellationToken)
        {
            try
            {
                context.Entry(order).State = EntityState.Modified;
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
                var order = await context.Orders.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                if (order is not null)
                {
                    context.Remove(order);
                    await context.SaveChangesAsync(cancellationToken);
                }
                return Result.Deleted;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<IEnumerable<OrderProduct>>> GetAllProductsByOrderIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var order = await context.Orders
                    .Include(e => e.OrderProducts)
                    .Include(e => e.Products)
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                return order?.OrderProducts ?? RepositoryErrors.NotFound.ToErrorOr<IEnumerable<OrderProduct>>();
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Updated>> AddProductToOrderAsync(Guid orderId, Guid productId, uint quantity, CancellationToken cancellationToken)
        {
            try
            {
                var order = await context.Orders
                    .Include(e => e.OrderProducts)
                    .FirstOrDefaultAsync(e => e.Id == orderId, cancellationToken);
                var product = await context.Products.FirstOrDefaultAsync(e => e.Id == productId, cancellationToken);
                if (product is not null && order is not null)
                {
                    if (order.OrderProducts.FirstOrDefault(op => op.ProductId == product.Id) is null)
                    {
                        order.OrderProducts.Add(new OrderProduct
                        {
                            OrderId = orderId,
                            ProductId = productId,
                            Quantity = quantity,
                        });
                        context.Update(order);
                        await context.SaveChangesAsync(cancellationToken);
                    }
                    return Result.Updated;
                }
                return RepositoryErrors.NotFound;
            }
            catch (OperationCanceledException) { return RepositoryErrors.Cancelled; }
            catch (Exception ex) { return RepositoryErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Deleted>> DeleteProductFromOrderAsync(Guid orderId, Guid productId, CancellationToken cancellationToken)
        {
            try
            {
                var order = await context.Orders
                    .Include(e => e.OrderProducts)
                    .FirstOrDefaultAsync(e => e.Id == orderId, cancellationToken);
                var orderProduct = order?.OrderProducts.FirstOrDefault(e => e.ProductId == productId);
                if (orderProduct is not null)
                {
                    if (order!.OrderProducts.Remove(orderProduct))
                    {
                        context.Update(order);
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
