using ErrorOr;
using MediatR;
    
namespace Application.Use_Cases.Authentication.Commands
{
    public class ChangeUserPasswordCommand : IRequest<ErrorOr<Success>>
    {
        public required Guid TokenId { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
