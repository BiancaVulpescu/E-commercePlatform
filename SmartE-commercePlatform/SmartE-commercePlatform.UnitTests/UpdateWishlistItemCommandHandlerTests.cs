using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace SmartE_commercePlatform.UnitTests
{
    public class UpdateWishlistItemCommandHandlerTests
    {
        private readonly IWishlistRepository repository;
        private readonly IMapper mapper;

        public UpdateWishlistItemCommandHandlerTests()
        {
            repository = Substitute.For<IWishlistRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async void Given_UpdateWishlistItemCommandHandler_When_HandleIsCalled_Then_WishlistItemShouldBeUpdatedAndShouldReturnNoContentAkaUnitValue()
        {
            //Arrange 
            var command = new UpdateWishlistCommand
            {
                Id = Guid.NewGuid(),
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };

            var wishlistItem = GenerateWishlistItem(command);
            GenerateWishlistItemDto(wishlistItem);

            repository.GetByIdAsync(command.Id).Returns(wishlistItem);
            var handler = new UpdateWishlistCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(Unit.Value);
            await repository.Received(1).UpdateAsync(wishlistItem);
        }

        [Fact]
        public async void Given_UpdateWishlistItemCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new UpdateWishlistCommand
            {
                Id = Guid.NewGuid(),
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };

            var wishlistItem = GenerateWishlistItem(command);
            GenerateWishlistItemDto(wishlistItem);

            repository.UpdateAsync(wishlistItem).Returns(Task.FromException<Unit>(new Exception("Exception thrown")));
            var handler = new UpdateWishlistCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be("Exception thrown");
        }
        [Fact]
        public async void Given_NullCommand_When_HandleIsCalled_Then_Given_NullCommand_When_HandleIsCalled_Then_ShouldThrowTheRequestIsNullFailure()
        {
            //Arrange 
            var command = new UpdateWishlistCommand
            {
                Id = Guid.NewGuid(),
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };
            mapper.Map<Wishlist>(command).Returns((Wishlist?)null);
            var handler = new UpdateWishlistCommandHandler(repository, mapper);

            //Act 
            var result = await handler.Handle(command, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be("The request is null");
        }

        private static Wishlist GenerateWishlistItem(UpdateWishlistCommand command)
        {
            var wishlistItem = new Wishlist
            {
                Id = command.Id,
                Product_Id = command.Product_Id,
                List_Id = command.List_Id
            };
            return wishlistItem;
        }
        private void GenerateWishlistItemDto(Wishlist wishlistItem)
        {
            mapper.Map<Wishlist>(Arg.Any<UpdateWishlistCommand>()).Returns(wishlistItem);

            mapper.Map<WishlistDto>(wishlistItem).Returns(new WishlistDto
            {
                Id = Guid.NewGuid(),
                Product_Id = wishlistItem.Product_Id,
                List_Id = wishlistItem.List_Id
            });
        }
    }
}
