using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.Queries
{
    public class GetUserProfileQuery : IRequest<ErrorOr<User>>
    {
        public Guid TokenId { get; set; }
    }
}