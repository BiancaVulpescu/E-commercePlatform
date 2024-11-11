using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using FluentAssertions;

namespace SmartE_commercePlatform.UnitTests
{
    public class CreateProductCommandHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public CreateProductCommandHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async void Given_CreateProductCommandHandler_When_HandleIsCalled_Then_ProductShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new CreateProductCommand
            {
                Title = "Product 1",
                Category = "category1",
                Description = "description1",
                Price = 100,
                IsNegotiable = true
            };

            var product = GenerateProduct(command);
            GenerateProductDto(product);

            repository.AddAsync(product).Returns(product.Id);
            var handler = new CreateProductCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(product.Id);
        }

        [Fact]
        public async void Given_CreateProductCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new CreateProductCommand
            {
                Title = "Product 1",
                Category = "category1",
                Description = "description1",
                Price = 100,
                IsNegotiable = true
            };

            var product = GenerateProduct(command);
            GenerateProductDto(product);

            repository.AddAsync(product).Returns(Task.FromException<Guid>(new Exception("Exception thrown")));
            var handler = new CreateProductCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be("Exception thrown");
        }
        //
        [Fact]
        public async void Given_NullCommand_When_HandleIsCalled_Then_ShouldThrowTheProductIsNullFailure()
        {
            //Arrange 
            var command = new CreateProductCommand
            {
                Title = "Product 1",
                Category = "category1",
                Description = "description1",
                Price = 100,
                IsNegotiable = true
            };
            mapper.Map<Product>(command).Returns((Product?)null);
            var handler = new CreateProductCommandHandler(repository, mapper);

            //Act 
            var result = await handler.Handle(command, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be("The product is null");
        }

        private static Product GenerateProduct(CreateProductCommand command)
        {
            var product = new Product
            {
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
            mapper.Map<Product>(Arg.Any<CreateProductCommand>()).Returns(product);

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