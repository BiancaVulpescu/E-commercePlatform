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
    public class UpdateShoppingCartItemCommandHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;
        public UpdateShoppingCartItemCommandHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async void Given_UpdateShoppingCartItemCommandHandler_When_HandleIsCalled_Then_ShoppingCartItemShouldBeUpdatedAndShouldReturnNoContentAkaUnitValue()
        {
            //Arrange 
            var command = new UpdateShoppingCartItemCommand
            {
                Id = Guid.NewGuid(),
                Product_Id = Guid.NewGuid(),
                Cart_Id = Guid.NewGuid(),
                Quantity = 2,
            };

            var shoppingCartItem = GenerateShoppingCartItem(command);
            GenerateShoppingCartItemDto(shoppingCartItem);

            repository.GetItemByIdAsync(command.Id).Returns(shoppingCartItem);
            var handler = new UpdateShoppingCartItemCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Unit.Value);
            await repository.Received(1).UpdateItemAsync(shoppingCartItem);
        }
        [Fact]
        public async void Given_UpdateShoppingCartItemCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new UpdateShoppingCartItemCommand
            {
                Id = Guid.NewGuid(),
                Product_Id = Guid.NewGuid(),
                Cart_Id = Guid.NewGuid(),
                Quantity = 2,
            };

            var shoppingCartItem = GenerateShoppingCartItem(command);
            GenerateShoppingCartItemDto(shoppingCartItem);

            repository.UpdateItemAsync(shoppingCartItem).Returns(Task.FromException<Unit>(new Exception()));
            var handler = new UpdateShoppingCartItemCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Description.Should().Be($"Shopping cart item with id {command.Id} not found.");
        }
        [Fact]
        public async void Given_NullCommand_When_HandleIsCalled_Then_Given_NullCommand_When_HandleIsCalled_Then_ShouldThrowTheRequestIsNullFailure()
        {
            //Arrange 
            var command = new UpdateShoppingCartItemCommand
            {
                Id = Guid.NewGuid(),
                Product_Id = Guid.NewGuid(),
                Cart_Id = Guid.NewGuid(),
                Quantity = 2,
            };
            mapper.Map<ShoppingCartItem>(command).Returns((ShoppingCartItem)null);
            var handler = new UpdateShoppingCartItemCommandHandler(repository, mapper);

            //Act 
            var result = await handler.Handle(command, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Description.Should().Be($"Shopping cart item with id {command.Id} not found.");
        }
        private ShoppingCartItem GenerateShoppingCartItem(UpdateShoppingCartItemCommand command)
        {
            var shoppingCartItem = new ShoppingCartItem
            {
                Id = Guid.NewGuid(),
                Product_Id = Guid.NewGuid(),
                Cart_Id = Guid.NewGuid(),
                Quantity = 1
            };
            return shoppingCartItem;
        }
        private void GenerateShoppingCartItemDto(ShoppingCartItem shoppingCartItem)
        {
            mapper.Map<ShoppingCartItem>(Arg.Any<UpdateShoppingCartItemCommand>()).Returns(shoppingCartItem);

            mapper.Map<ShoppingCartItemDto>(shoppingCartItem).Returns(new ShoppingCartItemDto
            {
                Id = Guid.NewGuid(),
                Product_Id = Guid.NewGuid(),
                Cart_Id = Guid.NewGuid(),
                Quantity = 1
            });
        }
        
    }
}
