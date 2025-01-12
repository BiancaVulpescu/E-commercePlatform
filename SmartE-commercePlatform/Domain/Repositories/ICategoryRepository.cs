using Domain.Entities;
using ErrorOr;

namespace Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        Task<ErrorOr<IEnumerable<Category>>> GetCategoriesByTitleAsync(string title, CancellationToken cancellationToken = default);
        Task<ErrorOr<IEnumerable<Category>>> GetAllParentCategoriesAsync(CancellationToken cancellationToken = default);
        Task<ErrorOr<IEnumerable<Category>>> GetByParentCategoryIdAsync(Guid ParentCategoryId, CancellationToken cancellationToken = default);
    }
}
