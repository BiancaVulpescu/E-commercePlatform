
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
    public class GetShoppingCartItemByIdQueryHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;
        public GetShoppingCartItemByIdQueryHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_GetShoppingCartItemByIdQueryHandler_When_HandleIsCalled_Then_ShouldReturnShoppingCartItemDto()
        {
            //Arrange 
            var query = new GetShoppingCartByIdQuery
            {
                Id = Guid.NewGuid()
            };

            var shoppingCartItem = GenerateShoppingCartItem(query.Id);
            GenerateShoppingCartItemDto(shoppingCartItem);

            repository.GetItemByIdAsync(query.Id).Returns(shoppingCartItem);
            var handler = new GetShoppingCartByIdQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Unwrap().Id.ToString().Should().Be(query.Id.ToString());
        }
        private static ShoppingCartProduct GenerateShoppingCartItem(Guid guid)
        {
            return new ShoppingCartProduct
            {
                Id = guid,
                Product_Id = Guid.NewGuid(),
                Cart_Id = Guid.NewGuid(),
                Quantity = 1
            };
        }

        private void GenerateShoppingCartItemDto(ShoppingCartProduct shoppingCartItem)
        {
            mapper.Map<ShoppingCartDto>(shoppingCartItem).Returns(new ShoppingCartDto
            {
                Id = shoppingCartItem.Id,
                Product_Id = shoppingCartItem.Product_Id,
                Cart_Id = shoppingCartItem.Cart_Id,
                Quantity = shoppingCartItem.Quantity
            });
        }
    }
}
