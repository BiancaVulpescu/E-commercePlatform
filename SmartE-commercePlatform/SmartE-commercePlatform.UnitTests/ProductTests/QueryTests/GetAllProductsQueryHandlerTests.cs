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
    public class GetAllProductsQueryHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;
        public GetAllProductsQueryHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_GetProductsQueryHandler_When_HandleIsCalled_Then_ShouldReturnProductDtoList()
        {
            //Arrange 
            var query = new GetAllProductsQuery();
            List<Product> products = GenerateProducts();
            GenerateProductDtos(products);

            repository.GetAllAsync().Returns(products);
            var handler = new GetAllProductsQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Value.Count().Should().Be(products.Count);
        }

        private static List<Product> GenerateProducts()
        {
            return [
                new() {
                    Id = Guid.NewGuid(),
                    Title = "Product 1",
                    Category = null,
                    Description = "description1",
                    Price = 100,
                },
                new() {
                    Id = Guid.NewGuid(),
                    Title = "Product 2",
                    Category = null,
                    Description = "description2",
                    Price = 200,
                }
            ];
        }

        private void GenerateProductDtos(List<Product> products)
        {
            mapper.Map<IEnumerable<ProductDtoMinimal>>(products).Returns([
                new() {
                    Id = products.ElementAt(0).Id,
                    Title = products.ElementAt(0).Title,
                    Category = null,
                    Description = products.ElementAt(0).Description,
                    Price = products.ElementAt(0).Price,
                },
                new() {
                    Id = products.ElementAt(1).Id,
                    Title = products.ElementAt(1).Title,
                    Category = null,
                    Description = products.ElementAt(1).Description,
                    Price = products.ElementAt(1).Price,
                }
            ]);
        }
    }
}
