using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace SmartE_commercePlatform.UnitTests.ShoppingCartTests.QueryTests
{
    public class GetAllShoppingCartsQueryHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;
        public GetAllShoppingCartsQueryHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_GetShoppingCartsQueryHandler_When_HandleIsCalled_Then_ShouldReturnShoppingCartDtoList()
        {
            //Arrange 
            var query = new GetAllShoppingCartsQuery();
            List<ShoppingCart> shoppingCarts = GenerateShoppingCarts();
            GenerateShoppingCartDtos(shoppingCarts);

            repository.GetAllAsync().Returns(shoppingCarts);
            var handler = new GetAllShoppingCartsQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Value.Count().Should().Be(shoppingCarts.Count);
        }

        private static List<ShoppingCart> GenerateShoppingCarts()
        {
            return [
                new() {
                    Id = Guid.NewGuid(),
                },
                new() {
                    Id = Guid.NewGuid(),
                }
            ];
        }

        private void GenerateShoppingCartDtos(List<ShoppingCart> shoppingCarts)
        {
            mapper.Map<IEnumerable<ShoppingCartDtoMinimal>>(shoppingCarts).Returns([
                new() {
                    Id = shoppingCarts.ElementAt(0).Id,
                },
                new() {
                    Id = shoppingCarts.ElementAt(1).Id,
                }
            ]);
        }
    }
}
