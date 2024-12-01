using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetAllWishlistsByProductIdQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetAllWishlistsByProductIdQuery, ErrorOr<IEnumerable<WishlistDtoMinimal>>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<IEnumerable<WishlistDtoMinimal>>> Handle(GetAllWishlistsByProductIdQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetAllWishlistsByProductIdAsync(request.Id, cancellationToken))
                .Then(mapper.Map<IEnumerable<WishlistDtoMinimal>>);
        }
    }
}
