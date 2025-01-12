using Application.Use_Cases.Authentication.Queries;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.QueryHandlers
{
    public class GetUserProfileQueryHandler(IUserRepository repository) : IRequestHandler<GetUserProfileQuery, ErrorOr<User>>
    {
        private readonly IUserRepository repository = repository;

        public async Task<ErrorOr<User>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetUserProfile(request.TokenId, cancellationToken);
        }
    }
}