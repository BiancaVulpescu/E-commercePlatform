using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.Commands
{
    public class LogoutCommand : IRequest<ErrorOr<Success>>
    {
        public Guid TokenId { get; set; }
        public required string RefreshSecret { get; set; }
    }
}
