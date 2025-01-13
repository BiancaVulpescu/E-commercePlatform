using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.Queries.Category;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers.Category
{
    public class GetAllParentCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper) : IRequestHandler<GetAllParentCategoriesQuery, ErrorOr<IEnumerable<CategoryDtoMinimal>>>
    {
        private readonly ICategoryRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<IEnumerable<CategoryDtoMinimal>>> Handle(GetAllParentCategoriesQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetAllParentCategoriesAsync(cancellationToken)).Then(mapper.Map<IEnumerable<CategoryDtoMinimal>>);
        }
    }
}
