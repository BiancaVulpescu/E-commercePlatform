using Application.DTOs;
using Application.Use_Cases.Commands;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace SmartE_commercePlatform.IntegrationTests
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly ApplicationDbContext dbContext;

        public ProductsControllerTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestProductManagementDb");
                    });
                });
            });

            var scope = this.factory.Services.CreateScope();
            dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetProducts_ReturnsSuccessStatusCodeAndListOfProducts()
        {
            // Arrange
            var client = factory.CreateClient();
            await AddTestProductAsync();

            // Act
            var response = await client.GetAsync("/api/v1/products");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");

            var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
            products.Should().NotBeNull();
            products.Should().HaveCount(1);
            products![0].Title.Should().Be("title1");
        }

        [Fact]
        public async Task GetProductById_ReturnsSuccessStatusCodeAndProduct()
        {
            // Arrange
            var client = factory.CreateClient();
            var product = await AddTestProductAsync();

            // Act
            var response = await client.GetAsync($"/api/v1/products/{product.Id}");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");

            var returnedProduct = await response.Content.ReadFromJsonAsync<ProductDto>();
            returnedProduct.Should().NotBeNull();
            returnedProduct.Should().BeEquivalentTo(product);
        }
        [Fact]
        public async Task GetProductById_ReturnsBadRequestStatusCode_WhenIdDoesntExist()
        {
            // Arrange
            var client = factory.CreateClient();
            var product = await AddTestProductAsync();

            // Act
            var response = await client.GetAsync($"/api/v1/products/{Guid.Empty}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task CreateProduct_ReturnsCreatedStatusCodeAndProductId()
        {
            // Arrange
            var client = factory.CreateClient();
            var command = new CreateProductCommand
            {
                Title = "title2",
                Description = "desc2",
                Category = "cat2",
                IsNegotiable = true,
                Price = 2.99m
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/products", command);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");
  
            Guid productId = await response.Content.ReadFromJsonAsync<Guid>();
        }
        [Fact]
        public async Task CreateProduct_ReturnsBadStatusCode_WhenTitleIsEmpty()
        {
            // Arrange
            var client = factory.CreateClient();
            var command = new CreateProductCommand
            {
                Title = "",
                Description = "desc2",
                Category = "cat2",
                IsNegotiable = true,
                Price = 2.99m
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/products", command);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        //[Fact]
        //public async Task CreateProduct_ReturnsBadStatusCode_WhenTitleIsMoreThan100Chars()
        //{
        //    // Arrange
        //    var client = factory.CreateClient();
        //    string bigString = Enumerable.Repeat('a', 101).ToString()!;
        //    var command = new CreateProductCommand
        //    {
        //        Title = bigString,
        //        Description = "desc2",
        //        Category = "cat2",
        //        IsNegotiable = true,
        //        Price = 2.99m
        //    };

        //    // Act
        //    var response = await client.PostAsJsonAsync("/api/v1/products", command);

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        //}
        [Fact]
        public async Task CreateProduct_ReturnsBadStatusCode_WhenDescriptionIsEmpty()
        {
            // Arrange
            var client = factory.CreateClient();
            var command = new CreateProductCommand
            {
                Title = "title2",
                Description = "",
                Category = "cat2",
                IsNegotiable = true,
                Price = 2.99m
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/products", command);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        //[Fact]
        //public async Task CreateProduct_ReturnsBadStatusCode_WhenDescriptionIsMoreThan200Chars()
        //{
        //    // Arrange
        //    var client = factory.CreateClient();
        //    string bigString = Enumerable.Repeat('a', 201).ToString()!;
        //    var command = new CreateProductCommand
        //    {
        //        Title = "title2",
        //        Description = bigString,
        //        Category = "cat2",
        //        IsNegotiable = true,
        //        Price = 2.99m
        //    };

        //    // Act
        //    var response = await client.PostAsJsonAsync("/api/v1/products", command);

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        //}
        [Fact]
        public async Task UpdateProduct_ReturnsNoContentStatusCodeAndProductIsChanged()
        {
            // Arrange
            var client = factory.CreateClient();
            var product = await AddTestProductAsync();
            var command = new UpdateProductCommand
            {
                Id = product.Id,
                Title = "title2",
                Description = "desc2",
                Category = "cat2",
                IsNegotiable = true,
                Price = 2.99m
            };

            // Act
            var response = await client.PutAsJsonAsync($"/api/v1/products/{product.Id}", command);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var updatedResponse = await client.GetAsync($"/api/v1/products/{product.Id}");

            var updatedProduct = await updatedResponse.Content.ReadFromJsonAsync<ProductDto>();
            updatedProduct.Should().NotBeNull();
            updatedProduct!.Title.Should().Be("title2");
        }
        [Fact]
        public async Task UpdateProduct_ReturnsBadRequestStatusCode_WhenURLIdDoesntMatchContent()
        {
            // Arrange
            var client = factory.CreateClient();
            var product = await AddTestProductAsync();
            var command = new UpdateProductCommand
            {
                Id = product.Id,
                Title = "title2",
                Description = "desc2",
                Category = "cat2",
                IsNegotiable = true,
                Price = 2.99m
            };

            // Act
            var response = await client.PutAsJsonAsync($"/api/v1/products/{Guid.Empty}", command);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task UpdateProduct_ReturnsBadRequestStatusCode_WhenIdDoesntExist()
        {
            // Arrange
            var client = factory.CreateClient();
            var product = await AddTestProductAsync();
            var command = new UpdateProductCommand
            {
                Id = Guid.Empty,
                Title = "title2",
                Description = "desc2",
                Category = "cat2",
                IsNegotiable = true,
                Price = 2.99m
            };

            // Act
            var response = await client.PutAsJsonAsync($"/api/v1/products/{Guid.Empty}", command);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task UpdateProduct_ReturnsBadRequestStatusCode_WhenTitleIsEmpty()
        {
            // Arrange
            var client = factory.CreateClient();
            var product = await AddTestProductAsync();
            var command = new UpdateProductCommand
            {
                Id = product.Id,
                Title = "",
                Description = "desc2",
                Category = "cat2",
                IsNegotiable = true,
                Price = 2.99m
            };

            // Act
            var response = await client.PutAsJsonAsync($"/api/v1/products/{product.Id}", command);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        //[Fact]
        //public async Task UpdateProduct_ReturnsBadRequestStatusCode_WhenTitleIsMoreThan100Chars()
        //{
        //    // Arrange
        //    var client = factory.CreateClient();
        //    var product = await AddTestProductAsync();
        //    string bigString = Enumerable.Repeat('a', 101).ToString()!;
        //    var command = new UpdateProductCommand
        //    {
        //        Id = product.Id,
        //        Title = bigString,
        //        Description = "desc2",
        //        Category = "cat2",
        //        IsNegotiable = true,
        //        Price = 2.99m
        //    };

        //    // Act
        //    var response = await client.PutAsJsonAsync($"/api/v1/products/{product.Id}", command);

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        //}
        [Fact]
        public async Task UpdateProduct_ReturnsBadRequestStatusCode_WhenDescriptionIsEmpty()
        {
            // Arrange
            var client = factory.CreateClient();
            var product = await AddTestProductAsync();
            var command = new UpdateProductCommand
            {
                Id = product.Id,
                Title = "title2",
                Description = "",
                Category = "cat2",
                IsNegotiable = true,
                Price = 2.99m
            };

            // Act
            var response = await client.PutAsJsonAsync($"/api/v1/products/{product.Id}", command);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        //[Fact]
        //public async Task UpdateProduct_ReturnsBadRequestStatusCode_WhenDescriptionIsMoreThan200Chars()
        //{
        //    // Arrange
        //    var client = factory.CreateClient();
        //    var product = await AddTestProductAsync();
        //    string bigString = Enumerable.Repeat('a', 201).ToString()!;
        //    var command = new UpdateProductCommand
        //    {
        //        Id = product.Id,
        //        Title = "title2",
        //        Description = bigString,
        //        Category = "cat2",
        //        IsNegotiable = true,
        //        Price = 2.99m
        //    };

        //    // Act
        //    var response = await client.PutAsJsonAsync($"/api/v1/products/{product.Id}", command);

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        //}
        [Fact]
        public async Task DeleteProduct_ReturnsNoContentStatusCodeAndProductIsDeleted()
        {
            // Arrange
            var client = factory.CreateClient();
            var product = await AddTestProductAsync();

            // Act
            var response = await client.DeleteAsync($"/api/v1/products/{product.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var responseAfterDelete = await client.GetAsync($"/api/v1/products/{product.Id}");
            responseAfterDelete.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task DeleteProduct_ReturnsBadRequestStatusCode_WhenProductDoesntExist()
        {
            // Arrange
            var client = factory.CreateClient();

            // Act
            var response = await client.DeleteAsync($"/api/v1/products/{Guid.Empty}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private async Task<Product> AddTestProductAsync()
        {
            var product = dbContext.Products.Add(new Product
            {
                Title = "title1",
                Description = "desc1",
                Category = "cat1",
                IsNegotiable = true,
                Price = 1.99m
            });
            await dbContext.SaveChangesAsync();
            return product.Entity;
        }
        
        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
