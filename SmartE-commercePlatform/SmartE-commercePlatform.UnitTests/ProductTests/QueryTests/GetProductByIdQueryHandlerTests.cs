using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace SmartE_commercePlatform.UnitTests.ProductTests.QueryTests
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;
        public GetProductByIdQueryHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_GetProductByIdQueryHandler_When_HandleIsCalled_Then_ShouldReturnProductDto()
        {
            //Arrange 
            var query = new GetProductByIdQuery
            {
                Id = Guid.NewGuid()
            };

            var product = GenerateProduct(query.Id);
            GenerateProductDto(product);

            repository.GetByIdAsync(query.Id).Returns(product);
            var handler = new GetProductByIdQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Value.Id.Should().Be(query.Id);
        }
        private static Product GenerateProduct(Guid guid)
        {
            return new()
            {
                Id = guid,
                Title = "Product 1",
                Category = null,
                Description = "description1",
                Price = 100,
            };
        }

        private void GenerateProductDto(Product product)
        {
            mapper.Map<ProductDto>(product).Returns(
                new ProductDto
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
