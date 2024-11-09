using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using static Application.Error;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateShoppingCartItemCommandHandler : IRequestHandler<UpdateShoppingCartItemCommand, Result<Unit>>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IMapper mapper;

        public UpdateShoppingCartItemCommandHandler(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(UpdateShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = await shoppingCartRepository.GetItemByIdAsync(request.Id);
                if (cartItem is null)
                {
                    return Result<Unit>.Failure(ShoppingCartItemErrors.NotFound(request.Id));
                }

                mapper.Map(request, cartItem);

                await shoppingCartRepository.UpdateItemAsync(cartItem);
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure(ShoppingCartItemErrors.UpdateItemFailed(ex.Message));
            }
        }
    }
}
