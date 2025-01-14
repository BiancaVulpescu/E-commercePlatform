using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using FluentAssertions;
using ErrorOr;
using Infrastructure.Errors;

namespace SmartE_commercePlatform.UnitTests.WishlistTests.CommandTests
{
    public class CreateWishlistCommandHandlerTests
    {
        private readonly IWishlistRepository repository;
        private readonly IMapper mapper;

        public CreateWishlistCommandHandlerTests()
        {
            repository = Substitute.For<IWishlistRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_CreateWishlistCommandHandler_When_HandleIsCalled_Then_WishlistShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new CreateWishlistCommand
            {
            };

            var wishlist = GenerateWishlist(command);
            GenerateWishlistDto(wishlist);

            repository.AddAsync(wishlist).Returns(wishlist.Id);
            var handler = new CreateWishlistCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(wishlist.Id);
        }
        [Fact]
        public async Task Given_RepositoryError_When_HandleIsCalled_Then_ErrorShouldBeReturned()
        {
            //Arrange 
            var command = new CreateWishlistCommand
            {
            };

            var wishlist = GenerateWishlist(command);
            SetupMapCommandToWishlist(wishlist);
            GenerateWishlistDto(wishlist);

            repository.AddAsync(wishlist).Returns(RepositoryErrors.NotFound);
            var handler = new CreateWishlistCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
        }

        private void SetupMapCommandToWishlist(Wishlist wishlist)
        {
            mapper.Map<Wishlist>(Arg.Any<CreateWishlistCommand>()).Returns(wishlist);
        }

        private static Wishlist GenerateWishlist(CreateWishlistCommand command)
        {
            var wishlist = new Wishlist
            {
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