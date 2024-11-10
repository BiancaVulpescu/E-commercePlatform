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
    public class DeleteProductCommandHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;
        public DeleteProductCommandHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_DeleteProductCommandHandler_When_HandleIsCalled_Then_ProductShouldBeDeletedAndShouldReturnNoContentAkaUnitValue()
        {
            //Arrange 
            var command = new DeleteProductCommand
            {
                Id = Guid.NewGuid()
            };

            var product = GenerateProduct(command.Id);
            GenerateProductDto(product);

            repository.GetByIdAsync(command.Id).Returns(product);
            var handler = new DeleteProductCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Unit.Value);
            await repository.Received(1).DeleteAsync(command.Id);
        }

        [Fact]
        public async Task Given_DeleteProductCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new DeleteProductCommand
            {
                Id = Guid.NewGuid()
            };

            var product = GenerateProduct(command.Id);
            GenerateProductDto(product);

            repository.GetByIdAsync(command.Id).Returns(product);
            repository.When(x => x.DeleteAsync(command.Id)).Throw(new Exception("Exception thrown"));
            var handler = new DeleteProductCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Description.Should().Be("Exception thrown");
        }

        [Fact]
        public async Task Given_ProductIdNotFound_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new DeleteProductCommand
            {
                Id = Guid.NewGuid()
            };

            repository.GetByIdAsync(command.Id).Returns((Product)null);
            var handler = new DeleteProductCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Description.Should().Be($"The product with id: {command.Id} was not found.");
        }

        private Product GenerateProduct(Guid id)
        {
            return new Product
            {
                Id = id,
                Title = "Product 1",
                Category = "category1",
                Description = "description1",
                Price = 100,
                IsNegotiable = true
            };
        }
        private void GenerateProductDto(Product product)
        {
            mapper.Map<ProductDto>(product).Returns(new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Category = product.Category,
                Description = product.Description,
                Price = product.Price,
                IsNegotiable = product.IsNegotiable
            });
        }
    }
}
