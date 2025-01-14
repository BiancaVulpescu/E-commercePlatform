using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace SmartE_commercePlatform.UnitTests.OrderTests.QueryTests
{
    public class GetOrderByIdQueryHandlerTests
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;
        public GetOrderByIdQueryHandlerTests()
        {
            repository = Substitute.For<IOrderRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_GetOrderByIdQueryHandler_When_HandleIsCalled_Then_ShouldReturnOrderDto()
        {
            //Arrange 
            var query = new GetOrderByIdQuery
            {
                Id = Guid.NewGuid()
            };

            var order = GenerateOrder(query.Id);
            GenerateOrderDto(order);

            repository.GetByIdAsync(query.Id).Returns(order);
            var handler = new GetOrderByIdQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Value.Id.Should().Be(query.Id);
        }
        private static Order GenerateOrder(Guid guid)
        {
            return new()
            {
                Id = guid,
            };
        }

        private void GenerateOrderDto(Order order)
        {
            mapper.Map<OrderDto>(order).Returns(
                new OrderDto
                {
                    Id = order.Id,
                });
        }
    }
}
