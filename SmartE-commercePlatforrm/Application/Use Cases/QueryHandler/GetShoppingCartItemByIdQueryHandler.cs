using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using static Application.Error;

namespace Application.Use_Cases.QueryHandler
{
    public class GetShoppingCartItemByIdQueryHandler : IRequestHandler<GetShoppingCartItemByIdQuery, Result<ShoppingCartItemsDto>>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetShoppingCartItemByIdQueryHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<ShoppingCartItemsDto>> Handle(GetShoppingCartItemByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = await repository.GetItemByIdAsync(request.Id);

                if (cartItem == null)
                {
                    return Result<ShoppingCartItemsDto>.Failure(ShoppingCartErrors.NotFound(request.Id));
                }

                var cartItemDto = mapper.Map<ShoppingCartItemsDto>(cartItem);
                return Result<ShoppingCartItemsDto>.Success(cartItemDto);
            }
            catch (Exception ex)
            {
                return Result<ShoppingCartItemsDto>.Failure(ShoppingCartErrors.GetItemFailed(ex.Message));
            }
        }
    }
}
