using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using FluentAssertions;
using Infrastructure.Errors;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartE_commercePlatform.UnitTests.WishlistTests.CommandTests
{
    public class UpdateWishlistCommandHandlerTests
    {
        private readonly IWishlistRepository repository;
        private readonly IMapper mapper;

        public UpdateWishlistCommandHandlerTests()
        {
            repository = Substitute.For<IWishlistRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_CreateWishlistCommandHandler_When_HandleIsCalled_Then_WishlistShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new UpdateWishlistCommand
            {
                Id = Guid.NewGuid(),
            };

            var wishlist = GenerateWishlist(command);
            SetupMapCommandToWishlist(wishlist);
            GenerateWishlistDto(wishlist);
            repository.UpdateAsync(wishlist).Returns(Result.Updated);
            var handler = new UpdateWishlistCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(Result.Updated);
        }
        [Fact]
        public async Task Given_RepositoryError_When_HandleIsCalled_Then_ErrorShouldBeReturned()
        {
            //Arrange 
            var command = new UpdateWishlistCommand
            {
                Id = Guid.NewGuid(),
            };

            var wishlist = GenerateWishlist(command);
            SetupMapCommandToWishlist(wishlist);
            GenerateWishlistDto(wishlist);
            repository.UpdateAsync(wishlist).Returns(RepositoryErrors.Unknown(new Exception()));
            var handler = new UpdateWishlistCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
        }
        private void SetupMapCommandToWishlist(Wishlist wishlist)
        {
            mapper.Map<Wishlist>(Arg.Any<UpdateWishlistCommand>()).Returns(wishlist);
        }

        private static Wishlist GenerateWishlist(UpdateWishlistCommand command)
        {
            var wishlist = new Wishlist
            {
                Id = command.Id,
            };
            return wishlist;
        }
        private void GenerateWishlistDto(Wishlist wishlist)
        {
            mapper.Map<WishlistDto>(wishlist).Returns(new WishlistDto
            {
                Id = wishlist.Id,
            });
        }
    }
}
