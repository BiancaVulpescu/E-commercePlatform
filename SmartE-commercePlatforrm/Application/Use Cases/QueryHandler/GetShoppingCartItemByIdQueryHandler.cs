using Application.DTOs;
using Application.Errors;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using static Application.Error;

namespace Application.Use_Cases.QueryHandler
{
    public class GetShoppingCartItemByIdQueryHandler : IRequestHandler<GetShoppingCartItemByIdQuery, Result<ShoppingCartItemDto>>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetShoppingCartItemByIdQueryHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<ShoppingCartItemDto>> Handle(GetShoppingCartItemByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = await repository.GetItemByIdAsync(request.Id);

                if (cartItem == null)
                {
                    return Result<ShoppingCartItemDto>.Failure(ShoppingCartItemErrors.NotFound(request.Id));
                }

                var cartItemDto = mapper.Map<ShoppingCartItemDto>(cartItem);
                return Result<ShoppingCartItemDto>.Success(cartItemDto);
            }
            catch (Exception ex)
            {
                return Result<ShoppingCartItemDto>.Failure(ShoppingCartItemErrors.GetItemFailed(ex.Message));
            }
        }
    }
}
