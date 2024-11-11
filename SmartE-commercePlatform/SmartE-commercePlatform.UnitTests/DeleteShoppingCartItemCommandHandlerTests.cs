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
            var command = new DeleteShoppingCartItemCommand
            {
                Id = Guid.NewGuid()
            };

            var shoppingCartItem = GenerateShoppingCartItem(command.Id);
            GenerateShoppingCartItemDto(shoppingCartItem);

            repository.GetItemByIdAsync(command.Id).Returns(shoppingCartItem);
            var handler = new DeleteShoppingCartItemCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Unit.Value);
            await repository.Received(1).RemoveItemAsync(command.Id);
        }
        [Fact]
        public async Task Given_DeleteShoppingCartItemCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new DeleteShoppingCartItemCommand
            {
                Id = Guid.NewGuid()
            };

            var shoppingCartItem = GenerateShoppingCartItem(command.Id);
            GenerateShoppingCartItemDto(shoppingCartItem);

            repository.GetItemByIdAsync(command.Id).Returns(shoppingCartItem);
            repository.When(x => x.RemoveItemAsync(command.Id)).Throw(new Exception("Exception thrown"));
            var handler = new DeleteShoppingCartItemCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error!.Description.Should().Be("Failed to delete shopping cart item. Exception thrown");
        }

        [Fact]
        public async Task Given_ShoppingCartItemIdNotFound_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new DeleteShoppingCartItemCommand
            {
                Id = Guid.NewGuid()
            };

            repository.GetItemByIdAsync(command.Id).Returns((ShoppingCartItem?)null);
            var handler = new DeleteShoppingCartItemCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error!.Description.Should().Be($"Shopping cart item with id {command.Id} not found.");
        }
        private static ShoppingCartItem GenerateShoppingCartItem(Guid id)
        {
            return new ShoppingCartItem
            {
                Id = id,
                Product_Id = Guid.NewGuid(),
                Cart_Id = Guid.NewGuid(),
            };
        }
        private void GenerateShoppingCartItemDto(ShoppingCartItem shoppingCartItem)
        {
            mapper.Map<ShoppingCartItemDto>(shoppingCartItem).Returns(new ShoppingCartItemDto
            {
                Id = shoppingCartItem.Id,
                Product_Id = shoppingCartItem.Product_Id,
                Cart_Id = shoppingCartItem.Cart_Id
            });
        }
    }
}
