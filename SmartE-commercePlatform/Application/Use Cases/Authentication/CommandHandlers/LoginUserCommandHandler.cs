using Application.Use_Cases.Authentication.Commands;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.CommandHandlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ErrorOr<string>>
    {
        private readonly IUserRepository userRepository;

        public LoginUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                PasswordHash = request.Password
            };
            var token = await userRepository.Login(user);
            return token;
        }
    }

}
