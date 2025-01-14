using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using FluentAssertions;
using ErrorOr;
using Infrastructure.Errors;

namespace SmartE_commercePlatform.UnitTests.ShoppingCartTests.CommandTests
{
    public class CreateShoppingCartCommandHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public CreateShoppingCartCommandHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_CreateShoppingCartCommandHandler_When_HandleIsCalled_Then_ShoppingCartShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new CreateShoppingCartCommand { };

            var shoppingCart = GenerateShoppingCart(command);
            GenerateShoppingCartDto(shoppingCart);

            repository.AddAsync(shoppingCart).Returns(shoppingCart.Id);
            var handler = new CreateShoppingCartCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(shoppingCart.Id);
        }
        [Fact]
        public async Task Given_RepositoryError_When_HandleIsCalled_Then_ErrorShouldBeReturned()
        {
            //Arrange 
            var command = new CreateShoppingCartCommand { };

            var shoppingCart = GenerateShoppingCart(command);
            SetupMapCommandToShoppingCart(shoppingCart);
            GenerateShoppingCartDto(shoppingCart);

            repository.AddAsync(shoppingCart).Returns(RepositoryErrors.NotFound);
            var handler = new CreateShoppingCartCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
        }

        private void SetupMapCommandToShoppingCart(ShoppingCart shoppingCart)
        {
            mapper.Map<ShoppingCart>(Arg.Any<CreateShoppingCartCommand>()).Returns(shoppingCart);
        }

        private static ShoppingCart GenerateShoppingCart(CreateShoppingCartCommand command)
        {
            var shoppingCart = new ShoppingCart { };
            return shoppingCart;
        }
        private void GenerateShoppingCartDto(ShoppingCart shoppingCart)
        {
            mapper.Map<ShoppingCartDto>(shoppingCart).Returns(new ShoppingCartDto
            {
                Id = shoppingCart.Id,
            });
        }
    }
}