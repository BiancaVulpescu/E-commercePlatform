using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using FluentAssertions;
using ErrorOr;
using Infrastructure.Errors;

namespace SmartE_commercePlatform.UnitTests.ProductTests.CommandTests
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
        public async Task Given_CreateProductCommandHandler_When_HandleIsCalled_Then_ProductShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new CreateProductCommand
            {
                Title = "Product 1",
                Description = "description1",
                Price = 100,
                CategoryID = null,
            };

            var product = GenerateProduct(command);
            GenerateProductDto(product);

            repository.AddAsync(product).Returns(product.Id);
            var handler = new CreateProductCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(product.Id);
        }
        [Fact]
        public async Task Given_RepositoryError_When_HandleIsCalled_Then_ErrorShouldBeReturned()
        {
            //Arrange 
            var command = new CreateProductCommand
            {
                Title = "Product 1",
                Description = "description1",
                Price = 100,
                CategoryID = null,
            };

            var product = GenerateProduct(command);
            SetupMapCommandToProduct(product);
            GenerateProductDto(product);

            repository.AddAsync(product).Returns(RepositoryErrors.NotFound);
            var handler = new CreateProductCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
        }

        private void SetupMapCommandToProduct(Product product)
        {
            mapper.Map<Product>(Arg.Any<CreateProductCommand>()).Returns(product);
        }

        private static Product GenerateProduct(CreateProductCommand command)
        {
            var product = new Product
            {
                Title = command.Title,
                Description = command.Description,
                Price = command.Price,
                CategoryId = command.CategoryID,
            };
            return product;
        }
        private void GenerateProductDto(Product product)
        {
            mapper.Map<ProductDto>(product).Returns(new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Category = null,
                Description = product.Description,
                Price = product.Price,
            });
        }
    }
}