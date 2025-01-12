using Application.Use_Cases.Authentication.Queries;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.QueryHandlers
{
    public class GetUserCartsIdQueryHandler(IUserRepository repository) : IRequestHandler<GetUserCartsIdQuery, ErrorOr<Guid>>
    {
        private readonly IUserRepository repository = repository;

        public async Task<ErrorOr<Guid>> Handle(GetUserCartsIdQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetUserCartsId(request.TokenId, cancellationToken);
        }
    }
}