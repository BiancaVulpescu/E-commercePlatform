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

namespace SmartE_commercePlatform.UnitTests.ProductTests.CommandTests
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly IProductRepository repository;

        public DeleteProductCommandHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
        }
        [Fact]
        public async Task Given_DeleteProductCommandHandler_When_HandleIsCalled_Then_ProductShouldBeDeleted()
        {
            //Arrange 
            var command = new DeleteProductCommand
            {
                Id = Guid.NewGuid(),
            };

            repository.DeleteAsync(command.Id).Returns(Result.Deleted);
            var handler = new DeleteProductCommandHandler(repository);

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
            var command = new DeleteProductCommand
            {
                Id = Guid.NewGuid(),
            };

            repository.DeleteAsync(command.Id).Returns(RepositoryErrors.Unknown(new Exception()));
            var handler = new DeleteProductCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
        }
    }
}
