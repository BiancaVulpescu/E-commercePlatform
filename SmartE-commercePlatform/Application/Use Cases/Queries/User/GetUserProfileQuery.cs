using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.Queries
{
    public class GetUserProfileQuery : IRequest<ErrorOr<User>>
    {
        public required Guid TokenId { get; set; }
    }
}