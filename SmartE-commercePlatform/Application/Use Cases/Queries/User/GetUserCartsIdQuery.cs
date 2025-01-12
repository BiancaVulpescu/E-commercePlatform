using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.Queries
{
    public class GetUserCartsIdQuery : IRequest<ErrorOr<Guid>>
    {
        public required Guid TokenId { get; set; }
    }
}