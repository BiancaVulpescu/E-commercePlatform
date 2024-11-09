using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandler
{
    public class GetAllWishlistItemsQueryHandler : IRequestHandler<GetAllWishlistItemsQuery, List<WishlistItemDto>>
    {
        private readonly IWishlistItemRepository repository;
        private readonly IMapper mapper;

        public GetAllWishlistItemsQueryHandler(IWishlistItemRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<WishlistItemDto>> Handle(GetAllWishlistItemsQuery request, CancellationToken cancellationToken)
        {
            var wishlistItems = await repository.GetAllAsync();
            return mapper.Map<List<WishlistItemDto>>(wishlistItems);
        }
    }
}
