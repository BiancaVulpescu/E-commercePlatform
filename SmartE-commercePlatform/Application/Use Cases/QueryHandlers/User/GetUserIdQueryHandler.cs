using Application.Use_Cases.Authentication.Queries;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.QueryHandlers
{
    public class GetUserIdQueryHandler(IUserRepository repository) : IRequestHandler<GetUserIdQuery, ErrorOr<Guid>>
    {
        private readonly IUserRepository repository = repository;

        public async Task<ErrorOr<Guid>> Handle(GetUserIdQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetUserId(request.TokenId, cancellationToken);
        }
    }
}