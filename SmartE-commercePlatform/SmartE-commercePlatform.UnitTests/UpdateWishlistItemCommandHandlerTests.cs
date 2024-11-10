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
        private readonly IWishlistItemRepository repository;
        private readonly IMapper mapper;

        public UpdateWishlistItemCommandHandlerTests()
        {
            repository = Substitute.For<IWishlistItemRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async void Given_UpdateWishlistItemCommandHandler_When_HandleIsCalled_Then_WishlistItemShouldBeUpdatedAndShouldReturnNoContentAkaUnitValue()
        {
            //Arrange 
            var command = new UpdateWishlistItemCommand
            {
                Id = Guid.NewGuid(),
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };

            var wishlistItem = GenerateWishlistItem(command);
            GenerateWishlistItemDto(wishlistItem);

            repository.GetByIdAsync(command.Id).Returns(wishlistItem);
            var handler = new UpdateWishlistItemCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Unit.Value);
            await repository.Received(1).UpdateAsync(wishlistItem);
        }

        [Fact]
        public async void Given_UpdateWishlistItemCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new UpdateWishlistItemCommand
            {
                Id = Guid.NewGuid(),
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };

            var wishlistItem = GenerateWishlistItem(command);
            GenerateWishlistItemDto(wishlistItem);

            repository.UpdateAsync(wishlistItem).Returns(Task.FromException<Unit>(new Exception("Exception thrown")));
            var handler = new UpdateWishlistItemCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Description.Should().Be("Exception thrown");
        }
        [Fact]
        public async void Given_NullCommand_When_HandleIsCalled_Then_Given_NullCommand_When_HandleIsCalled_Then_ShouldThrowTheRequestIsNullFailure()
        {
            //Arrange 
            var command = new UpdateWishlistItemCommand
            {
                Id = Guid.NewGuid(),
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };
            mapper.Map<WishlistItem>(command).Returns((WishlistItem)null);
            var handler = new UpdateWishlistItemCommandHandler(repository, mapper);

            //Act 
            var result = await handler.Handle(command, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Description.Should().Be("The request is null");
        }

        private WishlistItem GenerateWishlistItem(UpdateWishlistItemCommand command)
        {
            var wishlistItem = new WishlistItem
            {
                Id = command.Id,
                Product_Id = command.Product_Id,
                List_Id = command.List_Id
            };
            return wishlistItem;
        }
        private void GenerateWishlistItemDto(WishlistItem wishlistItem)
        {
            mapper.Map<WishlistItem>(Arg.Any<UpdateWishlistItemCommand>()).Returns(wishlistItem);

            mapper.Map<WishlistItemDto>(wishlistItem).Returns(new WishlistItemDto
            {
                Id = Guid.NewGuid(),
                Product_Id = wishlistItem.Product_Id,
                List_Id = wishlistItem.List_Id
            });
        }
    }
}
