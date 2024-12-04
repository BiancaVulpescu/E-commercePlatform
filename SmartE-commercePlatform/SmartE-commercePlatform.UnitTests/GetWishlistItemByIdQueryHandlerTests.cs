using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace SmartE_commercePlatform.UnitTests
{
    public class GetWishlistItemByIdQueryHandlerTests
    {
        private readonly IWishlistRepository repository;
        private readonly IMapper mapper;
        public GetWishlistItemByIdQueryHandlerTests()
        {
            repository = Substitute.For<IWishlistRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_GetWishlistItemByIdQueryHandler_When_HandleIsCalled_Then_ShouldReturnWishlistItemDto()
        {
            //Arrange 
            var query = new GetWishlistByIdQuery
            {
                Id = Guid.NewGuid()
            };

            var wishlistItem = GenerateWishlistItem(query.Id);
            GenerateWishlistItemDto(wishlistItem);

            repository.GetByIdAsync(query.Id).Returns(wishlistItem);
            var handler = new GetWishlistByIdQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Unwrap().Id.ToString().Should().Be(query.Id.ToString());
        }
        private static Wishlist GenerateWishlistItem(Guid guid)
        {
            return new Wishlist
            {
                Id = guid,
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid()
            };
        }
        private void GenerateWishlistItemDto(Wishlist wishlistItem)
        {
            mapper.Map<WishlistDto>(wishlistItem).Returns(new WishlistDto
            {
                Id = wishlistItem.Id,
                Product_Id = wishlistItem.Product_Id,
                List_Id = wishlistItem.List_Id
            });
        }
    }
}
