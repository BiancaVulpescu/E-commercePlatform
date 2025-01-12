using Application.Use_Cases.Authentication.Queries;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.QueryHandlers
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ErrorOr<UserDto>>
    {
        private readonly IUserRepository repository;

        public GetUserProfileQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ErrorOr<UserDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userResult = await repository.GetUserProfile(request.TokenId, cancellationToken);
            return userResult.Match<ErrorOr<UserDto>>(
                user => new UserDto { Id = user.Id, Email = user.Email, PasswordHash = user.PasswordHash },
                error => error
            );
        }
    }
}
