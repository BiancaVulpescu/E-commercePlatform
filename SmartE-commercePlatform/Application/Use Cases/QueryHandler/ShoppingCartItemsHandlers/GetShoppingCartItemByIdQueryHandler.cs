using Application.DTOs;
using Application.Errors;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandler
{
    public class GetShoppingCartItemByIdQueryHandler : IRequestHandler<GetShoppingCartItemByIdQuery, Result<ShoppingCartItemDto, ShoppingCartItemError>>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetShoppingCartItemByIdQueryHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<ShoppingCartItemDto, ShoppingCartItemError>> Handle(GetShoppingCartItemByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = await repository.GetItemByIdAsync(request.Id);

                if (cartItem == null)
                {
                    return Result<ShoppingCartItemDto, ShoppingCartItemError>.Err(ShoppingCartItemError.NotFound(request.Id));
                }

                var cartItemDto = mapper.Map<ShoppingCartItemDto>(cartItem);
                return Result<ShoppingCartItemDto, ShoppingCartItemError>.Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return Result<ShoppingCartItemDto, ShoppingCartItemError>.Err(ShoppingCartItemError.GetItemFailed(ex.Message));
            }
        }
    }
}
