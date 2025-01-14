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
    public class GetShoppingCartByIdQueryHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;
        public GetShoppingCartByIdQueryHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_GetShoppingCartByIdQueryHandler_When_HandleIsCalled_Then_ShouldReturnShoppingCartDto()
        {
            //Arrange 
            var query = new GetShoppingCartByIdQuery
            {
                Id = Guid.NewGuid()
            };

            var shoppingCart = GenerateShoppingCart(query.Id);
            GenerateShoppingCartDto(shoppingCart);

            repository.GetByIdAsync(query.Id).Returns(shoppingCart);
            var handler = new GetShoppingCartByIdQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Value.Id.Should().Be(query.Id);
        }
        private static ShoppingCart GenerateShoppingCart(Guid guid)
        {
            return new()
            {
                Id = guid,
            };
        }

        private void GenerateShoppingCartDto(ShoppingCart shoppingCart)
        {
            mapper.Map<ShoppingCartDto>(shoppingCart).Returns(
                new ShoppingCartDto
                {
                    Id = shoppingCart.Id,
                });
        }
    }
}
