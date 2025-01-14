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
    public class GetAllOrdersQueryHandlerTests
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;
        public GetAllOrdersQueryHandlerTests()
        {
            repository = Substitute.For<IOrderRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_GetOrdersQueryHandler_When_HandleIsCalled_Then_ShouldReturnOrderDtoList()
        {
            //Arrange 
            var query = new GetAllOrdersQuery();
            List<Order> orders = GenerateOrders();
            GenerateOrderDtos(orders);

            repository.GetAllAsync().Returns(orders);
            var handler = new GetAllOrdersQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert 
            result.Should().NotBeNull();
            result.Value.Count().Should().Be(orders.Count);
        }

        private static List<Order> GenerateOrders()
        {
            return [
                new() {
                    Id = Guid.NewGuid(),
                },
                new() {
                    Id = Guid.NewGuid(),
                }
            ];
        }

        private void GenerateOrderDtos(List<Order> orders)
        {
            mapper.Map<IEnumerable<OrderDtoMinimal>>(orders).Returns([
                new() {
                    Id = orders.ElementAt(0).Id,
                },
                new() {
                    Id = orders.ElementAt(1).Id,
                }
            ]);
        }
    }
}
