using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

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
                await shoppingCartRepository.UpdateItemAsync(mapper.Map<ShoppingCartItem>(request));
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure(ShoppingCartItemErrors.UpdateItemFailed(ex.Message));
            }
        }
    }
}
