using Domain.Entities;
using ErrorOr;

namespace Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        Task<ErrorOr<IEnumerable<OrderProduct>>> GetAllProductsByOrderIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ErrorOr<IEnumerable<Order>>> GetAllOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<ErrorOr<Updated>> AddProductToOrderAsync(Guid orderId, Guid productId, uint quantity, CancellationToken cancellationToken = default);
        Task<ErrorOr<Deleted>> DeleteProductFromOrderAsync(Guid orderId, Guid productId, CancellationToken cancellationToken = default);
    }
}
