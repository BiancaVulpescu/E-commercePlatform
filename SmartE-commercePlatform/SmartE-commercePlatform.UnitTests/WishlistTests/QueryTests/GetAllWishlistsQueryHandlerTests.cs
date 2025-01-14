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
    public class GetAllWishlistsQueryHandlerTests
    {
        private readonly IWishlistRepository repository;
        private readonly IMapper mapper;
        public GetAllWishlistsQueryHandlerTests()
        {
            repository = Substitute.For<IWishlistRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_GetWishlistsQueryHandler_When_HandleIsCalled_Then_ShouldReturnWishlistDtoList()
        {
            //Arrange 
            var query = new GetAllWishlistsQuery();
            List<Wishlist> wishlists = GenerateWishlists();
            GenerateWishlistDtos(wishlists);

            repository.GetAllAsync().Returns(wishlists);
            var handler = new GetAllWishlistsQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Value.Count().Should().Be(wishlists.Count);
        }

        private static List<Wishlist> GenerateWishlists()
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

        private void GenerateWishlistDtos(List<Wishlist> wishlists)
        {
            mapper.Map<IEnumerable<WishlistDtoMinimal>>(wishlists).Returns([
                new() {
                    Id = wishlists.ElementAt(0).Id,
                },
                new() {
                    Id = wishlists.ElementAt(1).Id,
                }
            ]);
        }
    }
}
