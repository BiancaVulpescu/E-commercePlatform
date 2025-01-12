using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using Identity.Errors;
using Identity.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using Microsoft.ML;

namespace Identity.Repositories
{
    public class UserRepository(UsersDbContext context, IConfiguration configuration) : IUserRepository
    {
        private readonly UsersDbContext context = context;
        private readonly IConfiguration configuration = configuration;

        public async Task<ErrorOr<LoginResponse>> Login(User user, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email, cancellationToken);
                if (existingUser == null || !BCrypt.Net.BCrypt.Verify(user.PasswordHash, existingUser.PasswordHash))
                { 
                    return AuthenticationErrors.InvalidCredentials;
                }

                string refreshSecret = Guid.NewGuid().ToString();
                var refreshToken = new RefreshToken
                {
                    UserId = existingUser.Id,
                    SecretHash = BCrypt.Net.BCrypt.HashPassword(refreshSecret),
                    ExpiresAt = DateTimeOffset.Now.AddDays(180),
                };
                context.RefreshTokens.Add(refreshToken);
                await context.SaveChangesAsync(cancellationToken);

                DateTime accessExpiresAt = DateTime.UtcNow.AddMinutes(15);
                string accessToken = GenerateJWT(existingUser.Id, accessExpiresAt);
                return new LoginResponse { 
                    RefreshTokenId = refreshToken.Id,
                    RefreshToken = refreshSecret,
                    AccessToken = accessToken,
                    ExpiresAt = accessExpiresAt
                };
            }
            catch (OperationCanceledException) { return AuthenticationErrors.Cancelled; }
            catch (Exception ex) { return AuthenticationErrors.Unknown(ex); }
        }

        private string GenerateJWT(Guid userId, DateTime expiration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Name, userId.ToString())
                    ]),
                Expires = expiration,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<ErrorOr<Guid>> Register(User user, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email, cancellationToken);
                if (existingUser != null)
                {
                    return AuthenticationErrors.EmailAlreadyExists;
                }
                context.Users.Add(user);
                await context.SaveChangesAsync(cancellationToken);
                return user.Id;
            }
            catch (OperationCanceledException) { return AuthenticationErrors.Cancelled; }
            catch (Exception ex) { return AuthenticationErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<RefreshResponse>> Refresh(Guid tokenId, string refreshSecret, CancellationToken cancellationToken = default)
        {
            try
            {
                var refreshToken = await context.RefreshTokens.FindAsync([tokenId], cancellationToken);
                if (refreshToken == null || !BCrypt.Net.BCrypt.Verify(refreshSecret, refreshToken.SecretHash))
                {
                    return AuthenticationErrors.InvalidCredentials;
                }

                DateTime accessExpiresAt = DateTime.UtcNow.AddMinutes(15);
                string accessToken = GenerateJWT(refreshToken.UserId, accessExpiresAt);
                return new RefreshResponse
                {
                    AccessToken = accessToken,
                    ExpiresAt = accessExpiresAt
                };
            }
            catch (OperationCanceledException) { return AuthenticationErrors.Cancelled; }
            catch (Exception ex) { return AuthenticationErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Success>> Logout(Guid tokenId, string refreshSecret, CancellationToken cancellationToken = default)
        {
            try
            {
                var refreshToken = await context.RefreshTokens.FindAsync([tokenId], cancellationToken);
                if (refreshToken == null || !BCrypt.Net.BCrypt.Verify(refreshSecret, refreshToken.SecretHash))
                {
                    return AuthenticationErrors.InvalidCredentials;
                }

                context.RefreshTokens.Remove(refreshToken);
                await context.SaveChangesAsync(cancellationToken);
                return Result.Success;
            }
            catch (OperationCanceledException) { return AuthenticationErrors.Cancelled; }
            catch (Exception ex) { return AuthenticationErrors.Unknown(ex); }
        }

        public async Task<ErrorOr<Success>> LogoutAll(Guid tokenId, string refreshSecret, CancellationToken cancellationToken = default)
        {
            try
            {
                var refreshToken = await context.RefreshTokens.FindAsync([tokenId], cancellationToken);
                if (refreshToken == null || !BCrypt.Net.BCrypt.Verify(refreshSecret, refreshToken.SecretHash))
                {
                    return AuthenticationErrors.InvalidCredentials;
                }

                await context.RefreshTokens.Where(e => e.UserId == refreshToken.UserId).ExecuteDeleteAsync(cancellationToken);
                return Result.Success;
            }
            catch (OperationCanceledException) { return AuthenticationErrors.Cancelled; }
            catch (Exception ex) { return AuthenticationErrors.Unknown(ex); }
        }
        public async Task<ErrorOr<Success>> ChangeUserPassword(Guid tokenId, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
        {
            try
            {
                var refreshToken = await context.RefreshTokens.FindAsync([tokenId], cancellationToken);
                if (refreshToken == null)
                {
                    return AuthenticationErrors.InvalidCredentials;
                }
                var user = await context.Users.FindAsync([refreshToken.UserId], cancellationToken);
                if (user == null || !BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                {
                    return AuthenticationErrors.InvalidCredentials;
                }
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                await context.SaveChangesAsync(cancellationToken);
                return Result.Success;
            }
            catch (OperationCanceledException)
            { 
                return AuthenticationErrors.Cancelled; 
            }
            catch (Exception ex)
            {
                return AuthenticationErrors.Unknown(ex);
            }
        }
        public async Task<ErrorOr<User>> GetUserProfile(Guid tokenId, CancellationToken cancellationToken = default)
        {
            try
            {
                var refreshToken = await context.RefreshTokens.FindAsync(new object[] { tokenId }, cancellationToken);
                if (refreshToken == null)
                {
                    return AuthenticationErrors.InvalidCredentials;
                }

                var user = await context.Users.FindAsync(new object[] { refreshToken.UserId }, cancellationToken);
                if (user == null)
                {
                    return AuthenticationErrors.InvalidCredentials;
                }

                return user;
            }
            catch (OperationCanceledException)
            {
                return AuthenticationErrors.Cancelled;
            }
            catch (Exception ex)
            {
                return AuthenticationErrors.Unknown(ex);
            }
        }

    }
}
