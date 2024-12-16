using Application.Use_Cases.Authentication.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.Commands
{
    public class RefreshAccessCommand : IRequest<ErrorOr<RefreshResponseDto>>
    {
        public Guid TokenId { get; set; }
        public required string RefreshSecret { get; set; }
    }
}
