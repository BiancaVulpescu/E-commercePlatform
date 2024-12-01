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
    public class GetAllShoppingCartItemsQueryHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;
        public GetAllShoppingCartItemsQueryHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_GetAllShoppingCartItemsQueryHandler_When_HandleIsCalled_Then_ShouldReturnShoppingCartItemDtoList()
        {
            //Arrange 
            var query = new GetAllShoppingCartsQuery
            {
                CartId = Guid.NewGuid()
            };

            var shoppingCartItems = GenerateShoppingCartItems(query.CartId);
            GenerateShoppingCartItemDtos(shoppingCartItems);

            //repository.GetAllItemsAsync(query.CartId).Returns(shoppingCartItems);
            repository.GetAllItemsAsync().Returns(shoppingCartItems);
            var handler = new GetAllShoppingCartsQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Count.Should().Be(shoppingCartItems.Count);
        }
        private static List<ShoppingCartProduct> GenerateShoppingCartItems(Guid guid)
        {
            return new List<ShoppingCartProduct>
            {
                new ShoppingCartProduct
                {
                    Id = Guid.NewGuid(),
                    Product_Id = Guid.NewGuid(),
                    Cart_Id = guid,
                    Quantity = 1
                },
                new ShoppingCartProduct
                {
                    Id = Guid.NewGuid(),
                    Product_Id = Guid.NewGuid(),
                    Cart_Id = guid,
                    Quantity = 2
                }
            };
        }

        private void GenerateShoppingCartItemDtos(List<ShoppingCartProduct> shoppingCartItems)
        {
            mapper.Map<List<ShoppingCartDto>>(Arg.Any<List<ShoppingCartProduct>>()).Returns(new List<ShoppingCartDto>
            {
                new ShoppingCartDto
                {
                    Id = shoppingCartItems[0].Id,
                    Product_Id = shoppingCartItems[0].Product_Id,
                    Cart_Id = shoppingCartItems[0].Cart_Id,
                    Quantity = shoppingCartItems[0].Quantity
                },
                new ShoppingCartDto
                {
                    Id = shoppingCartItems[1].Id,
                    Product_Id = shoppingCartItems[1].Product_Id,
                    Cart_Id = shoppingCartItems[1].Cart_Id,
                    Quantity = shoppingCartItems[1].Quantity
                }
            });
        }
    }
}
