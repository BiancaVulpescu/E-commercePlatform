using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.Queries.Category;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers.Category
{
    public class GetCategoriesByParentIdQueryHandler(ICategoryRepository repository, IMapper mapper) : IRequestHandler<GetCategoriesByParentIdQuery, ErrorOr<IEnumerable<CategoryDto>>>
    {
        private readonly ICategoryRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<IEnumerable<CategoryDto>>> Handle(GetCategoriesByParentIdQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetByParentCategoryIdAsync(request.ParentCategoryId, cancellationToken)).Then(mapper.Map<IEnumerable<CategoryDto>>);
        }
    }
}
