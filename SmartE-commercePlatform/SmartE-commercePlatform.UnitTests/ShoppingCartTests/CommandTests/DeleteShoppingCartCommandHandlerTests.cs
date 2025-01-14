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
    public class DeleteShoppingCartCommandHandlerTests
    {
        private readonly IShoppingCartRepository repository;

        public DeleteShoppingCartCommandHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
        }
        [Fact]
        public async Task Given_DeleteShoppingCartCommandHandler_When_HandleIsCalled_Then_ShoppingCartShouldBeDeleted()
        {
            //Arrange 
            var command = new DeleteShoppingCartCommand
            {
                Id = Guid.NewGuid(),
            };

            repository.DeleteAsync(command.Id).Returns(Result.Deleted);
            var handler = new DeleteShoppingCartCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(Result.Deleted);
        }
        [Fact]
        public async Task Given_RepositoryError_When_HandleIsCalled_Then_ErrorShouldBeReturned()
        {
            //Arrange 
            var command = new DeleteShoppingCartCommand
            {
                Id = Guid.NewGuid(),
            };

            repository.DeleteAsync(command.Id).Returns(RepositoryErrors.Unknown(new Exception()));
            var handler = new DeleteShoppingCartCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
        }
    }
}
