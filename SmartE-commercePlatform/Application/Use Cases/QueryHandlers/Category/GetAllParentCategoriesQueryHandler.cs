//using Application.DTOs;
//using Application.Use_Cases.Queries;
//using AutoMapper;
//using Domain.Repositories;
//using ErrorOr;
//using MediatR;

//namespace Application.Use_Cases.QueryHandlers.Category
//{
//    public class GetAllParentCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, ErrorOr<IEnumerable<CategoryDto>>>
//    {
//        private readonly ICategoryRepository repository = repository;
//        private readonly IMapper mapper = mapper;

//        public async Task<ErrorOr<IEnumerable<CategoryDtoMinimal>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
//        {
//            return (await repository.GetAllAsync(cancellationToken)).Then(mapper.Map<IEnumerable<CategoryDtoMinimal>>);
//        }
//    }
//}
