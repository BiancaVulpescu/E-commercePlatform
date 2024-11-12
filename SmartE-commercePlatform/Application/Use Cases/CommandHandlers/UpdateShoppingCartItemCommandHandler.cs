using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Common;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateShoppingCartItemCommandHandler : IRequestHandler<UpdateShoppingCartItemCommand, Result<Unit, ShoppingCartItemError>>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IMapper mapper;

        public UpdateShoppingCartItemCommandHandler(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Unit, ShoppingCartItemError>> Handle(UpdateShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = await shoppingCartRepository.GetItemByIdAsync(request.Id);
                if (cartItem is null)
                {
                    return Result<Unit, ShoppingCartItemError>.Err(ShoppingCartItemError.NotFound(request.Id));
                }

                mapper.Map(request, cartItem);

                await shoppingCartRepository.UpdateItemAsync(cartItem);
                return Result<Unit, ShoppingCartItemError>.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit, ShoppingCartItemError>.Err(ShoppingCartItemError.UpdateItemFailed(ex.Message));
            }
        }
    }
}
