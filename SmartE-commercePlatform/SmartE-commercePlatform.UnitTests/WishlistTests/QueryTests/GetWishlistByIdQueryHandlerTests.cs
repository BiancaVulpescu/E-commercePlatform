using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace SmartE_commercePlatform.UnitTests.WishlistTests.QueryTests
{
    public class GetWishlistByIdQueryHandlerTests
    {
        private readonly IWishlistRepository repository;
        private readonly IMapper mapper;
        public GetWishlistByIdQueryHandlerTests()
        {
            repository = Substitute.For<IWishlistRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_GetWishlistByIdQueryHandler_When_HandleIsCalled_Then_ShouldReturnWishlistDto()
        {
            //Arrange 
            var query = new GetWishlistByIdQuery
            {
                Id = Guid.NewGuid()
            };

            var wishlist = GenerateWishlist(query.Id);
            GenerateWishlistDto(wishlist);

            repository.GetByIdAsync(query.Id).Returns(wishlist);
            var handler = new GetWishlistByIdQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Value.Id.Should().Be(query.Id);
        }
        private static Wishlist GenerateWishlist(Guid guid)
        {
            return new()
            {
                Id = guid,
            };
        }

        private void GenerateWishlistDto(Wishlist wishlist)
        {
            mapper.Map<WishlistDto>(wishlist).Returns(
                new WishlistDto
                {
                    Id = wishlist.Id,
                });
        }
    }
}
