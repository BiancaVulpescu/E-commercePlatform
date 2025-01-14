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

namespace SmartE_commercePlatform.UnitTests.OrderTests.CommandTests
{
    public class UpdateOrderCommandHandlerTests
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;

        public UpdateOrderCommandHandlerTests()
        {
            repository = Substitute.For<IOrderRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public async Task Given_CreateOrderCommandHandler_When_HandleIsCalled_Then_OrderShouldBeCreatedAndReturnGuid()
        {
            //Arrange 
            var command = new UpdateOrderCommand
            {
                Id = Guid.NewGuid(),
            };

            var order = GenerateOrder(command);
            SetupMapCommandToOrder(order);
            GenerateOrderDto(order);
            repository.UpdateAsync(order).Returns(Result.Updated);
            var handler = new UpdateOrderCommandHandler(repository, mapper);

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
            var command = new UpdateOrderCommand
            {
                Id = Guid.NewGuid(),
            };

            var order = GenerateOrder(command);
            SetupMapCommandToOrder(order);
            GenerateOrderDto(order);
            repository.UpdateAsync(order).Returns(RepositoryErrors.Unknown(new Exception()));
            var handler = new UpdateOrderCommandHandler(repository, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
        }
        private void SetupMapCommandToOrder(Order order)
        {
            mapper.Map<Order>(Arg.Any<UpdateOrderCommand>()).Returns(order);
        }

        private static Order GenerateOrder(UpdateOrderCommand command)
        {
            var order = new Order
            {
                Id = command.Id,
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
