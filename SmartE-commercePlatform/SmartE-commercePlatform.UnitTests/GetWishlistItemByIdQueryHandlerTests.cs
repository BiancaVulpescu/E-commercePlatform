using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartE_commercePlatform.UnitTests
{
    public class GetWishlistItemByIdQueryHandlerTests
    {
        private readonly IWishlistItemRepository repository;
        private readonly IMapper mapper;
        public GetWishlistItemByIdQueryHandlerTests()
        {
            repository = Substitute.For<IWishlistItemRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_GetWishlistItemByIdQueryHandler_When_HandleIsCalled_Then_ShouldReturnWishlistItemDto()
        {
            //Arrange 
            var query = new GetWishlistItemByIdQuery
            {
                Id = Guid.NewGuid()
            };

            var wishlistItem = GenerateWishlistItem(query.Id);
            GenerateWishlistItemDto(wishlistItem);

            repository.GetByIdAsync(query.Id).Returns(wishlistItem);
            var handler = new GetWishlistItemByIdQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Value.Id.ToString().Should().Be(query.Id.ToString());
        }
        private WishlistItem GenerateWishlistItem(Guid guid)
        {
            return new WishlistItem
            {
                Id = guid,
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid()
            };
        }
        private void GenerateWishlistItemDto(WishlistItem wishlistItem)
        {
            mapper.Map<WishlistItemDto>(wishlistItem).Returns(new WishlistItemDto
            {
                Id = wishlistItem.Id,
                Product_Id = wishlistItem.Product_Id,
                List_Id = wishlistItem.List_Id
            });
        }
    }
}
