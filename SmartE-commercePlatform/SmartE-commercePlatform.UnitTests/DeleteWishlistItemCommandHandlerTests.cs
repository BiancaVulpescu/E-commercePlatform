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
    public class DeleteWishlistItemCommandHandlerTests
    {
        private readonly IWishlistRepository repository;
        private readonly IMapper mapper;
        public DeleteWishlistItemCommandHandlerTests()
        {
            repository = Substitute.For<IWishlistRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_DeleteWishlistItemCommandHandler_When_HandleIsCalled_Then_WishlistItemShouldBeDeletedAndShouldReturnNoContentAkaUnitValue()
        {
            //Arrange 
            var command = new DeleteWishlistCommand
            {
                Id = Guid.NewGuid()
            };

            var wishlistItem = GenerateWishlistItem(command.Id);
            GenerateWishlistItemDto(wishlistItem);

            repository.GetByIdAsync(command.Id).Returns(wishlistItem);
            var handler = new DeleteWishlistCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(Unit.Value);
            await repository.Received(1).DeleteAsync(command.Id);
        }
        [Fact]
        public async Task Given_DeleteWishlistItemCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new DeleteWishlistCommand
            {
                Id = Guid.NewGuid()
            };

            var wishlistItem = GenerateWishlistItem(command.Id);
            GenerateWishlistItemDto(wishlistItem);

            repository.GetByIdAsync(command.Id).Returns(wishlistItem);
            repository.When(x => x.DeleteAsync(command.Id)).Throw(new Exception("Exception thrown"));
            var handler = new DeleteWishlistCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be("Exception thrown");
        }

        [Fact]
        public async Task Given_DeleteWishlistItemIdNotFound_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new DeleteWishlistCommand
            {
                Id = Guid.NewGuid()
            };

            repository.GetByIdAsync(command.Id).Returns((Wishlist?)null);
            var handler = new DeleteWishlistCommandHandler(repository);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsOk.Should().BeFalse();
            result.UnwrapErr().Description.Should().Be($"The wishlist item with id: {command.Id} was not found.");
        }

        private static Wishlist GenerateWishlistItem(Guid id)
        {
            return new Wishlist
            {
                Id = id,
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
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
