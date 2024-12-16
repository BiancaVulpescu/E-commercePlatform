using Application.Use_Cases.Authentication.Commands;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.CommandHandlers
{

    public class LogoutAllCommandHandler(IUserRepository repository) : IRequestHandler<LogoutAllCommand, ErrorOr<Success>>
    {
        private readonly IUserRepository repository = repository;

        public async Task<ErrorOr<Success>> Handle(LogoutAllCommand request, CancellationToken cancellationToken)
        {
            return await repository.LogoutAll(request.TokenId, request.RefreshSecret, cancellationToken);
        }
    }
}
