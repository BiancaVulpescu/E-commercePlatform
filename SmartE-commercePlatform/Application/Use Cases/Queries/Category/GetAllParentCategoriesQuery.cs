using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries.Category
{
    public class GetAllParentCategoriesQuery : IRequest<ErrorOr<IEnumerable<CategoryDtoMinimal>>>
    {
    }
}
