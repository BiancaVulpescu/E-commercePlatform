using ErrorOr;

namespace Domain.Repositories
{
    public interface IRepository<TEntity, TId>
    {
        Task<ErrorOr<IEnumerable<TEntity>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ErrorOr<TEntity>> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<ErrorOr<TId>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<ErrorOr<Updated>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<ErrorOr<Deleted>> DeleteAsync(TId id, CancellationToken cancellationToken = default);
    }
}
