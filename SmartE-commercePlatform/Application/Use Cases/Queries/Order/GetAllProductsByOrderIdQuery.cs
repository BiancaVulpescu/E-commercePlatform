using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllProductsByOrderIdQuery : IRequest<ErrorOr<IEnumerable<OrderProductDtoP>>>
    {
        public Guid Id { get; set; }
    }
}
