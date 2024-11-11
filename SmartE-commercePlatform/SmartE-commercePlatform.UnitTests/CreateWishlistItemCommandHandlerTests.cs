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
        private readonly IWishlistItemRepository repository;
        private readonly IMapper mapper;

        public CreateWishlistItemCommandHandlerTests()
        {
            repository = Substitute.For<IWishlistItemRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async void Given_CreateWishlistItemCommandHandler_When_HandleIsCalled_Then_WishlistItemShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new CreateWishlistItemCommand
            {
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };

            var wishlistItem = GenerateWishlistItem(command);
            GenerateWishlistItemDto(wishlistItem);

            repository.AddAsync(wishlistItem).Returns(wishlistItem.Id);
            var handler = new CreateWishlistItemCommandHandler(repository, mapper);
            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(wishlistItem.Id);
        }

        [Fact]
        public async void Given_CreateWishlistItemCommandHandlerWithExceptionThrownWithinTheFunction_When_HandleIsCalled_Then_ResultShouldBeFailureMessage()
        {
            //Arrange 
            var command = new CreateWishlistItemCommand
            {
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };

            var wishlistItem = GenerateWishlistItem(command);
            GenerateWishlistItemDto(wishlistItem);

            repository.AddAsync(wishlistItem).Returns(Task.FromException<Guid>(new Exception("Exception thrown")));
            var handler = new CreateWishlistItemCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error!.Description.Should().Be("Exception thrown");
        }
        [Fact]
        public async void Given_NullCommand_When_HandleIsCalled_Then_ShouldThrowTheWishlistItemIsNullFailure()
        {
            //Arrange 
            var command = new CreateWishlistItemCommand
            {
                Product_Id = Guid.NewGuid(),
                List_Id = Guid.NewGuid(),
            };
            mapper.Map<WishlistItem>(command).Returns((WishlistItem?)null);
            var handler = new CreateWishlistItemCommandHandler(repository, mapper);

            //Act 
            var result = await handler.Handle(command, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error!.Description.Should().Be("The wishlist item is null");
        }

        private static WishlistItem GenerateWishlistItem(CreateWishlistItemCommand command)
        {
            return new WishlistItem
            {
                Id = Guid.NewGuid(),
                Product_Id = command.Product_Id,
                List_Id = command.List_Id,
            };
        }

        private void GenerateWishlistItemDto(WishlistItem wishlistItem)
        {
            mapper.Map<WishlistItem>(Arg.Any<CreateWishlistItemCommand>()).Returns(wishlistItem);

            mapper.Map<WishlistItemDto>(wishlistItem).Returns(new WishlistItemDto
            {
                Id = wishlistItem.Id,
                Product_Id = wishlistItem.Product_Id,
                List_Id = wishlistItem.List_Id
            });
        }
    }
}
