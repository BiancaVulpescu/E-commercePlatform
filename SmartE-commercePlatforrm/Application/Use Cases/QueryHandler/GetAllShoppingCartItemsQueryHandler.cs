using Application.DTOs;
using Application.Errors;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using static Application.Error;

namespace Application.Use_Cases.QueryHandler
{
    public class GetAllShoppingCartItemsQueryHandler : IRequestHandler<GetAllShoppingCartItemsQuery, Result<List<ShoppingCartItemDto>>>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetAllShoppingCartItemsQueryHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<List<ShoppingCartItemDto>>> Handle(GetAllShoppingCartItemsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItems = await repository.GetAllItemsAsync();

                if (cartItems == null || !cartItems.Any())
                {
                    return Result<List<ShoppingCartItemDto>>.Failure(ShoppingCartItemErrors.NotFound(request.CartId));
                }

                var cartItemsDto = mapper.Map<List<ShoppingCartItemDto>>(cartItems);
                return Result<List<ShoppingCartItemDto>>.Success(cartItemsDto);
            }
            catch (Exception ex)
            {
                return Result<List<ShoppingCartItemDto>>.Failure(ShoppingCartItemErrors.GetItemFailed(ex.Message));
            }
        }
    }
}
