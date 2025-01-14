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

namespace SmartE_commercePlatform.UnitTests.WishlistTests.CommandTests
{
    public class DeleteWishlistCommandHandlerTests
    {
        private readonly IWishlistRepository repository;

        public DeleteWishlistCommandHandlerTests()
        {
            repository = Substitute.For<IWishlistRepository>();
        }
        [Fact]
        public async Task Given_DeleteWishlistCommandHandler_When_HandleIsCalled_Then_WishlistShouldBeDeleted()
        {
            //Arrange 
            var command = new DeleteWishlistCommand
            {
                Id = Guid.NewGuid(),
            };

            repository.DeleteAsync(command.Id).Returns(Result.Deleted);
            var handler = new DeleteWishlistCommandHandler(repository);

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
            var command = new DeleteWishlistCommand
            {
                Id = Guid.NewGuid(),
            };

            repository.DeleteAsync(command.Id).Returns(RepositoryErrors.Unknown(new Exception()));
            var handler = new DeleteWishlistCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
        }
    }
}
