using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllOrdersByUserIdQuery : IRequest<ErrorOr<IEnumerable<OrderDtoMinimal>>>
    {
        public Guid UserId { get; set; }
    }
}
