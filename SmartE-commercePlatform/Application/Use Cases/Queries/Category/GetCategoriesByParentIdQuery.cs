using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries.Category
{
    public class GetCategoriesByParentIdQuery : IRequest<ErrorOr<IEnumerable<CategoryDto>>>
    {
        public Guid ParentCategoryId { get; set; }
    }
}
