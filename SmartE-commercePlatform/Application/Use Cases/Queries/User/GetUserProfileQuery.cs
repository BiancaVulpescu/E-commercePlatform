using MediatR;
using ErrorOr;
using Domain.Entities;
using System.Security.Claims;

namespace Application.Use_Cases.Authentication.Queries
{
    public class GetUserProfileQuery : IRequest<ErrorOr<User>>
    {
        public ClaimsPrincipal User { get; }

        public GetUserProfileQuery(ClaimsPrincipal user)
        {
            User = user;
        }
    }
}