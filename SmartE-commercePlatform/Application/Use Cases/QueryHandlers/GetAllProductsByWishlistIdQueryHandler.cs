using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetAllProductsByWishlistIdQueryHandler(IWishlistRepository repository, IMapper mapper) : IRequestHandler<GetAllProductsByWishlistIdQuery, ErrorOr<IEnumerable<ProductDtoMinimal>>>
    {
        private readonly IWishlistRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<IEnumerable<ProductDtoMinimal>>> Handle(GetAllProductsByWishlistIdQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetAllProductsByWishlistIdAsync(request.Id, cancellationToken))
                .Then(mapper.Map<IEnumerable<ProductDtoMinimal>>);
        }
    }
}
