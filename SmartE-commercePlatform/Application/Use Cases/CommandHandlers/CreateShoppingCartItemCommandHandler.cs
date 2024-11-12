using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Common;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class CreateShoppingCartItemCommandHandler : IRequestHandler<CreateShoppingCartItemCommand, Result<Guid, ShoppingCartItemError>>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper _mapper;

        public CreateShoppingCartItemCommandHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<Guid, ShoppingCartItemError>> Handle(CreateShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = _mapper.Map<ShoppingCartItem>(request);
            if (cartItem is null)
            {
                return Result<Guid, ShoppingCartItemError>.Err(ShoppingCartItemError.ValidationFailed("The cart item is null"));
            }
            try
            {
                var returnedId = await repository.AddItemAsync(cartItem);
                return Result<Guid, ShoppingCartItemError>.Ok(returnedId);
            }
            catch (Exception e)
            {
                return Result<Guid, ShoppingCartItemError>.Err(ShoppingCartItemError.CreateItemFailed(e.Message)); 
            }
        }
    }
}
