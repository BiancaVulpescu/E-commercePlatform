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

namespace SmartE_commercePlatform.UnitTests.OrderTests.CommandTests
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;

        public CreateOrderCommandHandlerTests()
        {
            repository = Substitute.For<IOrderRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_CreateOrderCommandHandler_When_HandleIsCalled_Then_OrderShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new CreateOrderCommand
            {
            };

            var order = GenerateOrder(command);
            GenerateOrderDto(order);

            repository.AddAsync(order).Returns(order.Id);
            var handler = new CreateOrderCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(order.Id);
        }
        [Fact]
        public async Task Given_RepositoryError_When_HandleIsCalled_Then_ErrorShouldBeReturned()
        {
            //Arrange 
            var command = new CreateOrderCommand
            {
            };

            var order = GenerateOrder(command);
            SetupMapCommandToOrder(order);
            GenerateOrderDto(order);

            repository.AddAsync(order).Returns(RepositoryErrors.NotFound);
            var handler = new CreateOrderCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
        }

        private void SetupMapCommandToOrder(Order order)
        {
            mapper.Map<Order>(Arg.Any<CreateOrderCommand>()).Returns(order);
        }

        private static Order GenerateOrder(CreateOrderCommand command)
        {
            var order = new Order
            {
            };
            return order;
        }
        private void GenerateOrderDto(Order order)
        {
            mapper.Map<OrderDto>(order).Returns(new OrderDto
            {
                Id = order.Id,
            });
        }
    }
}