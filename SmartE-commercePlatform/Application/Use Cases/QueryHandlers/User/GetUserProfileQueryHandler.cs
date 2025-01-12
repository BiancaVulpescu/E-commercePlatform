using Application.Use_Cases.Authentication.Queries;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;
using System.Security.Claims;

namespace Application.Use_Cases.Authentication.QueryHandlers
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ErrorOr<User>>
    {
        private readonly IUserRepository _repository;

        public GetUserProfileQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<User>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            // Extract the user ID from the access token
            var userId = request.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Error.Validation("InvalidToken", "The provided token is invalid.");
            }

            return await _repository.GetUserProfile(Guid.Parse(userId), cancellationToken);
        }
    }
}