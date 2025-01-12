using Domain.Entities;
using ErrorOr;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ErrorOr<Guid>> Register(User user, CancellationToken cancellationToken = default);
        Task<ErrorOr<LoginResponse>> Login(User user, CancellationToken cancellationToken = default);
        Task<ErrorOr<RefreshResponse>> Refresh(Guid tokenId, string refreshSecret, CancellationToken cancellationToken = default);
        Task<ErrorOr<Success>> ChangeUserPassword(Guid tokenId, string currentPassword, string newPassword, CancellationToken cancellationToken = default);
        Task<ErrorOr<Success>> Logout(Guid tokenId, string refreshSecret, CancellationToken cancellationToken = default);
        Task<ErrorOr<Success>> LogoutAll(Guid tokenId, string refreshSecret, CancellationToken cancellationToken = default);
        Task<ErrorOr<UserDto>> GetUserProfile(Guid tokenId, CancellationToken cancellationToken = default);

    }
}
