using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using FluentAssertions;
using Infrastructure.Errors;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartE_commercePlatform.UnitTests.ShoppingCartTests.CommandTests
{
    public class UpdateShoppingCartCommandHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public UpdateShoppingCartCommandHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_CreateShoppingCartCommandHandler_When_HandleIsCalled_Then_ShoppingCartShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new UpdateShoppingCartCommand
            {
                Id = Guid.NewGuid(),
            };

            var shoppingCart = GenerateShoppingCart(command);
            SetupMapCommandToShoppingCart(shoppingCart);
            GenerateShoppingCartDto(shoppingCart);
            repository.UpdateAsync(shoppingCart).Returns(Result.Updated);
            var handler = new UpdateShoppingCartCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(Result.Updated);
        }
        [Fact]
        public async Task Given_RepositoryError_When_HandleIsCalled_Then_ErrorShouldBeReturned()
        {
            //Arrange 
            var command = new UpdateShoppingCartCommand
            {
                Id = Guid.NewGuid(),
            };

            var shoppingCart = GenerateShoppingCart(command);
            SetupMapCommandToShoppingCart(shoppingCart);
            GenerateShoppingCartDto(shoppingCart);
            repository.UpdateAsync(shoppingCart).Returns(RepositoryErrors.Unknown(new Exception()));
            var handler = new UpdateShoppingCartCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
        }
        private void SetupMapCommandToShoppingCart(ShoppingCart shoppingCart)
        {
            mapper.Map<ShoppingCart>(Arg.Any<UpdateShoppingCartCommand>()).Returns(shoppingCart);
        }

        private static ShoppingCart GenerateShoppingCart(UpdateShoppingCartCommand command)
        {
            var shoppingCart = new ShoppingCart
            {
                Id = command.Id,
            };
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
