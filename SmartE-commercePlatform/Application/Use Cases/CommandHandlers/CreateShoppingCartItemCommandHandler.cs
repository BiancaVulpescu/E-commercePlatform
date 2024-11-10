using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class CreateShoppingCartItemCommandHandler : IRequestHandler<CreateShoppingCartItemCommand, Result<Guid>>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper _mapper;

        public CreateShoppingCartItemCommandHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = _mapper.Map<ShoppingCartItem>(request);
            if (cartItem is null)
            {
                return Result<Guid>.Failure(ShoppingCartItemErrors.ValidationFailed("The cart item is null"));
            }
            try
            {
                var returnedId = await repository.AddItemAsync(cartItem);
                return Result<Guid>.Success(returnedId);
            }
            catch (Exception e)
            {
                return Result<Guid>.Failure(ShoppingCartItemErrors.CreateItemFailed(e.Message)); 
            }
        }
    }
}
