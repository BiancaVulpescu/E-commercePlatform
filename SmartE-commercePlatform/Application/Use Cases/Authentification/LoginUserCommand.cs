using ErrorOr;
using MediatR;

public class LoginUserCommand : IRequest<ErrorOr<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
