using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandler;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace SmartE_commercePlatform.UnitTests
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
            result.Count().Should().Be(products.Count());
        }

        private List<Product> GenerateProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Title = "Product 1",
                    Category = "category1",
                    Description = "description1",
                    Price = 100,
                    IsNegotiable = true
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Title = "Product 2",
                    Category = "category1",
                    Description = "description2",
                    Price = 200,
                    IsNegotiable = false
                }
            };
        }

        private void GenerateProductDtos(List<Product> products)
        {
            mapper.Map<List<ProductDto>>(products).Returns(new List<ProductDto>
            {
                new ProductDto
                {
                    Id = products.ElementAt(0).Id,
                    Title = products.ElementAt(0).Title,
                    Category = products.ElementAt(0).Category,
                    Description = products.ElementAt(0).Description,
                    Price = products.ElementAt(0).Price,
                    IsNegotiable = products.ElementAt(0).IsNegotiable
                },
                new ProductDto
                {
                    Id = products.ElementAt(1).Id,
                    Title = products.ElementAt(1).Title,
                    Category = products.ElementAt(1).Category,
                    Description = products.ElementAt(1).Description,
                    Price = products.ElementAt(1).Price,
                    IsNegotiable = products.ElementAt(1).IsNegotiable
                }
            });
        }
    }
}
