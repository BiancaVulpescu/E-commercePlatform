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
        public async Task Given_CreateProductCommandHandler_When_HandleIsCalled_Then_ProductShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Title = "Product 1",
                Description = "description1",
                Price = 100,
                CategoryID = null,
            };

            var product = GenerateProduct(command);
            SetupMapCommandToProduct(product);
            GenerateProductDto(product);
            repository.UpdateAsync(product).Returns(Result.Updated);
            var handler = new UpdateProductCommandHandler(repository, mapper);

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
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Title = "Product 1",
                Description = "description1",
                Price = 100,
                CategoryID = null,
            };

            var product = GenerateProduct(command);
            SetupMapCommandToProduct(product);
            GenerateProductDto(product);
            repository.UpdateAsync(product).Returns(RepositoryErrors.Unknown(new Exception()));
            var handler = new UpdateProductCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
        }
        private void SetupMapCommandToProduct(Product product)
        {
            mapper.Map<Product>(Arg.Any<UpdateProductCommand>()).Returns(product);
        }

        private static Product GenerateProduct(UpdateProductCommand command)
        {
            var product = new Product
            {
                Id = command.Id,
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
