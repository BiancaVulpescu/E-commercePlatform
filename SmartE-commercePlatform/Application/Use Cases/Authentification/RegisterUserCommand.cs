using ErrorOr;
using MediatR;

public class RegisterUserCommand : IRequest<ErrorOr<Guid>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
