using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using static Application.Error;

namespace Application.Use_Cases.QueryHandler
{
    public class GetAllShoppingCartItemsQueryHandler : IRequestHandler<GetAllShoppingCartItemsQuery, Result<List<ShoppingCartItemsDto>>>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetAllShoppingCartItemsQueryHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<List<ShoppingCartItemsDto>>> Handle(GetAllShoppingCartItemsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItems = await repository.GetAllItemsAsync(request.CartId);

                if (cartItems == null || !cartItems.Any())
                {
                    return Result<List<ShoppingCartItemsDto>>.Failure(ShoppingCartErrors.NotFound(request.CartId));
                }

                var cartItemsDto = mapper.Map<List<ShoppingCartItemsDto>>(cartItems);
                return Result<List<ShoppingCartItemsDto>>.Success(cartItemsDto);
            }
            catch (Exception ex)
            {
                return Result<List<ShoppingCartItemsDto>>.Failure(ShoppingCartErrors.GetItemsFailed(ex.Message));
            }
        }
    }
}
