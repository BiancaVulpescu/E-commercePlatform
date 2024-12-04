using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace SmartE_commercePlatform.UnitTests
{
    public class CreateWishlistItemCommandHandlerTests
    {
        private readonly IWishlistRepository repository;
        private readonly IMapper mapper;

        public CreateWishlistItemCommandHandlerTests()
        {
            repository = Substitute.For<IWishlistRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async void Given_CreateWishlistItemCommandHandler_When_HandleIsCalled_Then_WishlistItemShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new CreateWishlistCommand
            {
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };

            var wishlistItem = GenerateWishlistItem(command);
            GenerateWishlistItemDto(wishlistItem);

            repository.AddAsync(wishlistItem).Returns(wishlistItem.Id);
            var handler = new CreateWishlistCommandHandler(repository, mapper);
            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(wishlistItem.Id);
        }

        [Fact]
        public async void Given_CreateWishlistItemCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new CreateWishlistCommand
            {
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };

            var wishlistItem = GenerateWishlistItem(command);
            GenerateWishlistItemDto(wishlistItem);

            repository.AddAsync(wishlistItem).Returns(Task.FromException<Guid>(new Exception("Exception thrown")));
            var handler = new CreateWishlistCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be("Exception thrown");
        }
        [Fact]
        public async void Given_NullCommand_When_HandleIsCalled_Then_ShouldThrowTheWishlistItemIsNullFailure()
        {
            //Arrange 
            var command = new CreateWishlistCommand
            {
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };
            mapper.Map<Wishlist>(command).Returns((Wishlist?)null);
            var handler = new CreateWishlistCommandHandler(repository, mapper);

            //Act 
            var result = await handler.Handle(command, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be("The wishlist item is null");
        }

        private static Wishlist GenerateWishlistItem(CreateWishlistCommand command)
        {
            return new Wishlist
            {
                Id = Guid.NewGuid(),
                Product_Id = command.Product_Id,
                List_Id = command.List_Id,
            };
        }

        private void GenerateWishlistItemDto(Wishlist wishlistItem)
        {
            mapper.Map<Wishlist>(Arg.Any<CreateWishlistCommand>()).Returns(wishlistItem);

            mapper.Map<WishlistDto>(wishlistItem).Returns(new WishlistDto
            {
                Id = wishlistItem.Id,
                Product_Id = wishlistItem.Product_Id,
                List_Id = wishlistItem.List_Id
            });
        }
    }
}
