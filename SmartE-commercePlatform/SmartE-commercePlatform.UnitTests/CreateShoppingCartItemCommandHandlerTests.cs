﻿using Application.Use_Cases.Commands;
using Application.Use_Cases.CommandHandlers;
using AutoMapper;
using Domain.Repositories;
using NSubstitute;
using FluentAssertions;
using Domain.Entities;
using Application.DTOs;

namespace SmartE_commercePlatform.UnitTests
{
    public class CreateShoppingCartItemCommandHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public CreateShoppingCartItemCommandHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async void Given_CreateShoppingCartItemCommandHandler_When_HandleIsCalled_Then_ShoppingCartItemShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new CreateShoppingCartCommand
            {
                Product_Id = Guid.NewGuid(),
                Cart_Id = Guid.NewGuid(),
                Quantity = 1
            };

            var shoppingCartItem = GenerateShoppingCartItem(command);
            GenerateShoppingCartItemDto(shoppingCartItem);

            repository.AddItemAsync(shoppingCartItem).Returns(shoppingCartItem.Id);
            var handler = new CreateShoppingCartCommandHandler(repository, mapper);
            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(shoppingCartItem.Id);
        }

        [Fact]
        public async void Given_CreateShoppingCartItemCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new CreateShoppingCartCommand
            {
                Product_Id = Guid.NewGuid(),
                Cart_Id = Guid.NewGuid(),
                Quantity = 1
            };

            var shoppingCartItem = GenerateShoppingCartItem(command);
            GenerateShoppingCartItemDto(shoppingCartItem);

            repository.AddItemAsync(shoppingCartItem).Returns(Task.FromException<Guid>(new Exception("Exception thrown")));
            var handler = new CreateShoppingCartCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be("Failed to create shopping cart item. Exception thrown");
        }
        [Fact]
        public async void Given_NullCommand_When_HandleIsCalled_Then_ShouldThrowTheCartItemIsNullFailure()
        {
            //Arrange 
            var command = new CreateShoppingCartCommand
            {
                Product_Id = Guid.NewGuid(),
                Cart_Id = Guid.NewGuid(),
                Quantity = 1
            };
            mapper.Map<ShoppingCartProduct>(command).Returns((ShoppingCartProduct?)null);
            var handler = new CreateShoppingCartCommandHandler(repository, mapper);

            //Act 
            var result = await handler.Handle(command, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be("The cart item is null");
        }

        private static ShoppingCartProduct GenerateShoppingCartItem(CreateShoppingCartCommand command)
        {
            return new ShoppingCartProduct
            {
                Id = Guid.NewGuid(),
                Product_Id = command.Product_Id,
                Cart_Id = command.Cart_Id,
                Quantity = command.Quantity
            };
        }

        private void GenerateShoppingCartItemDto(ShoppingCartProduct shoppingCartItem)
        {
            mapper.Map<ShoppingCartProduct>(Arg.Any<CreateShoppingCartCommand>()).Returns(shoppingCartItem);
            
            mapper.Map<ShoppingCartDto>(shoppingCartItem).Returns(new ShoppingCartDto
            {
                Id = shoppingCartItem.Id,
                Product_Id = shoppingCartItem.Product_Id,
                Cart_Id = shoppingCartItem.Cart_Id,
                Quantity = shoppingCartItem.Quantity
            });
        }

    }
}
