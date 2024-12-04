using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace SmartE_commercePlatform.UnitTests
{
    public class DeleteShoppingCartItemCommandHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;
        public DeleteShoppingCartItemCommandHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_DeleteProductCommandHandler_When_HandleIsCalled_Then_ShoppingListItemShouldBeDeletedAndShouldReturnNoContentAkaUnitValue()
        {
            //Arrange 
            var command = new DeleteShoppingCartCommand
            {
                Id = Guid.NewGuid()
            };

            var shoppingCartItem = GenerateShoppingCartItem(command.Id);
            GenerateShoppingCartItemDto(shoppingCartItem);

            repository.GetItemByIdAsync(command.Id).Returns(shoppingCartItem);
            var handler = new DeleteShoppingCartCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(Unit.Value);
            await repository.Received(1).RemoveItemAsync(command.Id);
        }
        [Fact]
        public async Task Given_DeleteShoppingCartItemCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new DeleteShoppingCartCommand
            {
                Id = Guid.NewGuid()
            };

            var shoppingCartItem = GenerateShoppingCartItem(command.Id);
            GenerateShoppingCartItemDto(shoppingCartItem);

            repository.GetItemByIdAsync(command.Id).Returns(shoppingCartItem);
            repository.When(x => x.RemoveItemAsync(command.Id)).Throw(new Exception("Exception thrown"));
            var handler = new DeleteShoppingCartCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be("Failed to delete shopping cart item. Exception thrown");
        }

        [Fact]
        public async Task Given_ShoppingCartItemIdNotFound_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new DeleteShoppingCartCommand
            {
                Id = Guid.NewGuid()
            };

            repository.GetItemByIdAsync(command.Id).Returns((ShoppingCartProduct?)null);
            var handler = new DeleteShoppingCartCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be($"Shopping cart item with id {command.Id} not found.");
        }
        private static ShoppingCartProduct GenerateShoppingCartItem(Guid id)
        {
            return new ShoppingCartProduct
            {
                Id = id,
                Product_Id = Guid.NewGuid(),
                Cart_Id = Guid.NewGuid(),
            };
        }
        private void GenerateShoppingCartItemDto(ShoppingCartProduct shoppingCartItem)
        {
            mapper.Map<ShoppingCartDto>(shoppingCartItem).Returns(new ShoppingCartDto
            {
                Id = shoppingCartItem.Id,
                Product_Id = shoppingCartItem.Product_Id,
                Cart_Id = shoppingCartItem.Cart_Id
            });
        }
    }
}
