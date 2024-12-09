using Domain.Entities;
using ErrorOr;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ErrorOr<Guid>> Register(User user, CancellationToken cancellationToken);
        Task<ErrorOr<string>> Login(User user);
    }
}
