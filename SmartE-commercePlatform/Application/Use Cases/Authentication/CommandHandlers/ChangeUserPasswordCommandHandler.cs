using Application.Use_Cases.Authentication.Commands;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.CommandHandlers
{
    public class ChangeUserPasswordCommandHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<ChangeUserPasswordCommand, ErrorOr<Success>>
    {
        private readonly IUserRepository repository = repository;
        public async Task<ErrorOr<Success>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            return await repository.ChangeUserPassword(request.TokenId, request.CurrentPassword, request.NewPassword, cancellationToken);
        }
    }

}
