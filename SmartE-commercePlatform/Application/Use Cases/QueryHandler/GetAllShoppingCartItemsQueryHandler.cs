using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandler
{
    public class GetAllShoppingCartItemsQueryHandler : IRequestHandler<GetAllShoppingCartItemsQuery, List<ShoppingCartItemDto>>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetAllShoppingCartItemsQueryHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ShoppingCartItemDto>> Handle(GetAllShoppingCartItemsQuery request, CancellationToken cancellationToken)
        {
                var cartItems = await repository.GetAllItemsAsync();
                return mapper.Map<List<ShoppingCartItemDto>>(cartItems);
          
           
        }
    }
}
