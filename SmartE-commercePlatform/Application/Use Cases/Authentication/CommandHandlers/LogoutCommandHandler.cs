using Application.Use_Cases.Authentication.Commands;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.CommandHandlers
{
    public class LogoutCommandHandler(IUserRepository repository) : IRequestHandler<LogoutCommand, ErrorOr<Success>>
    {
        private readonly IUserRepository repository = repository;

        public async Task<ErrorOr<Success>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            return await repository.Logout(request.TokenId, request.RefreshSecret, cancellationToken);
        }
    }
}
