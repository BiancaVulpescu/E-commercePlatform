using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.Commands
{
    public class RegisterUserCommand : IRequest<ErrorOr<Guid>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
