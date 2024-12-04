using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAllProductsQuery : IRequest<ErrorOr<IEnumerable<ProductDtoMinimal>>>
    {
    }
}
