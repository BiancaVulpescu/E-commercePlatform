﻿using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandler;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace SmartE_commercePlatform.UnitTests
{
    public class GetAllWishlistItemsQueryHandlerTests
    {
        private readonly IWishlistRepository repository;
        private readonly IMapper mapper;
        public GetAllWishlistItemsQueryHandlerTests()
        {
            repository = Substitute.For<IWishlistRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_GetAllWishlistItemsQueryHandler_When_HandleIsCalled_Then_ShouldReturnWishlistItemDtoList()
        {
            //Arrange 
            var query = new GetAllWishlistsQuery();

            var wishlistItems = GenerateWishlistItems();
            GenerateWishlistItemDtos(wishlistItems);

            repository.GetAllAsync().Returns(wishlistItems);
            var handler = new GetAllWishlistsQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Count.Should().Be(wishlistItems.Count);
        }
        private static List<Wishlist> GenerateWishlistItems()
        {
            return new List<Wishlist>
            {
                new Wishlist
                {
                    Id = Guid.NewGuid(),
                    Product_Id = Guid.NewGuid(),
                    List_Id = Guid.NewGuid()
                },
                new Wishlist
                {
                    Id = Guid.NewGuid(),
                    Product_Id = Guid.NewGuid(),
                    List_Id = Guid.NewGuid()
                }
            };
        }
        private void GenerateWishlistItemDtos(List<Wishlist> wishlistItems)
        {
            var wishlistItemDtos = wishlistItems.Select(item => new WishlistDto
            {
                Id = item.Id,
                Product_Id = item.Product_Id,
                List_Id = item.List_Id
            }).ToList();

            mapper.Map<List<WishlistDto>>(wishlistItems).Returns(wishlistItemDtos);
        }

    }
}
