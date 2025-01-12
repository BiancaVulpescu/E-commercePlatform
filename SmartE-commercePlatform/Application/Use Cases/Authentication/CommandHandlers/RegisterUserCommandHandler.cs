using Application.Use_Cases.Authentication.Commands;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<Guid>>
    {
        private readonly IUserRepository userRepository;
        private readonly IWishlistRepository wishlistRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository, IWishlistRepository wishlistRepository, IShoppingCartRepository shoppingCartRepository)
        {
            this.userRepository = userRepository;
            this.wishlistRepository = wishlistRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            var result = await userRepository.Register(user, cancellationToken);
            if (result.IsError)
            {
                return result;
            }

            var wishlist = new Wishlist { Id = user.CartsId };
            var shoppingCart = new ShoppingCart { Id = user.CartsId };

            await wishlistRepository.AddAsync(wishlist, cancellationToken);
            await shoppingCartRepository.AddAsync(shoppingCart, cancellationToken);

            return result;
        }
    }
}