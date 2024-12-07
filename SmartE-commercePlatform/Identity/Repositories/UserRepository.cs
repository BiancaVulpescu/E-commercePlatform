using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext usersDbContext;
        private readonly IConfiguration configuration;

        public UserRepository(UsersDbContext usersDbContext, IConfiguration configuration)
        {
            this.usersDbContext=usersDbContext;
            this.configuration=configuration;
        }

        public async Task<string> Login(User user)
        {
            var existingUser = await usersDbContext.Users.SingleOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<Guid> Register(User user, CancellationToken cancellationToken)
        {
            usersDbContext.Users.Add(user);
            await usersDbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
