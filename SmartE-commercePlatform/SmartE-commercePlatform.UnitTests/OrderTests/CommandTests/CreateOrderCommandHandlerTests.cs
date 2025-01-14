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
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public CreateOrderCommandHandlerTests()
        {
            orderRepository = Substitute.For<IOrderRepository>();
            mapper = Substitute.For<IMapper>();
            userRepository = Substitute.For<IUserRepository>();
        }
        [Fact]
        public async Task Given_CreateOrderCommandHandler_When_HandleIsCalled_Then_OrderShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new CreateOrderCommand
            {

                TokenId = Guid.NewGuid(),
                City = "New York",
                Address = "123 Example St",
                Status = "Pending"
            };

            var order = GenerateOrder(command);
            GenerateOrderDto(order);

            orderRepository.AddAsync(order).Returns(order.Id);
            var handler = new CreateOrderCommandHandler(orderRepository, mapper, userRepository);

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
                TokenId = Guid.NewGuid(),
                City = "New York",
                Address = "123 Example St",
                Status = "Pending"
            };

            var order = GenerateOrder(command);
            SetupMapCommandToOrder(order);
            GenerateOrderDto(order);

            orderRepository.AddAsync(order).Returns(RepositoryErrors.NotFound);
            var handler = new CreateOrderCommandHandler(orderRepository, mapper, userRepository);

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
                Id = Guid.NewGuid(),
                UserId = Guid.Empty,
                City = "New York",
                Address = "123 Example St",
                Status = "Pending"
            };
            return order;
        }
        private void GenerateOrderDto(Order order)
        {
            mapper.Map<OrderDto>(order).Returns(new OrderDto
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Empty,
                City = "New York",
                Address = "123 Example St",
                Status = "Pending"
            });
        }
    }
}