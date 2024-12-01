using ErrorOr;

namespace Domain.Repositories
{
    public interface IRepository<TEntity, TId>
    {
        Task<ErrorOr<IEnumerable<TEntity>>> GetAllAsync(CancellationToken cancellationToken);
        Task<ErrorOr<TEntity>> GetByIdAsync(TId id, CancellationToken cancellationToken);
        Task<ErrorOr<TId>> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<ErrorOr<Updated>> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<ErrorOr<Deleted>> DeleteAsync(TId id, CancellationToken cancellationToken);
    }
}
