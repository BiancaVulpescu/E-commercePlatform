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
    public class UpdateProductCommandHandlerTests
    {

        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public UpdateProductCommandHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async void Given_UpdateProductCommandHandler_When_HandleIsCalled_Then_ProductShouldBeUpdatedAndShouldReturnNoContentAkaUnitValue()
        {
            //Arrange 
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Title = "Product 1",
                Category = "category1",
                Description = "description1",
                Price = 100,
                IsNegotiable = true
            };

            var product = GenerateProduct(command);
            GenerateProductDto(product);

            repository.GetByIdAsync(command.Id).Returns(product);
            var handler = new UpdateProductCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Unit.Value);
            await repository.Received(1).UpdateAsync(product);
        }

        [Fact]
        public async void Given_UpdateProductCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Title = "Product 1",
                Category = "category1",
                Description = "description1",
                Price = 100,
                IsNegotiable = true
            };

            var product = GenerateProduct(command);
            GenerateProductDto(product);

            repository.UpdateAsync(product).Returns(Task.FromException<Unit>(new Exception("Exception thrown")));
            var handler = new UpdateProductCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Description.Should().Be("Exception thrown");
        }
        [Fact]
        public async void Given_NullCommand_When_HandleIsCalled_Then_Given_NullCommand_When_HandleIsCalled_Then_ShouldThrowTheRequestIsNullFailure()
        {
            //Arrange 
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Title = "Product 1",
                Category = "category1",
                Description = "description1",
                Price = 100,
                IsNegotiable = true
            };
            mapper.Map<Product>(command).Returns((Product)null);
            var handler = new UpdateProductCommandHandler(repository, mapper);

            //Act 
            var result = await handler.Handle(command, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Description.Should().Be("The request is null");
        }

        private Product GenerateProduct(UpdateProductCommand command)
        {
            var product = new Product
            {
                Id = command.Id,
                Title = command.Title,
                Category = command.Category,
                Description = command.Description,
                Price = command.Price,
                IsNegotiable = command.IsNegotiable
            };
            return product;
        }
        private void GenerateProductDto(Product product)
        {
            mapper.Map<Product>(Arg.Any<UpdateProductCommand>()).Returns(product);

            mapper.Map<ProductDto>(product).Returns(new ProductDto
            {
                Id = Guid.NewGuid(),
                Title = product.Title,
                Category = product.Category,
                Description = product.Description,
                Price = product.Price,
                IsNegotiable = product.IsNegotiable
            });
        }
    }
}
