using Application.Use_Cases.Authentication.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.Commands
{
    public class LoginUserCommand : IRequest<ErrorOr<LoginResponseDto>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
