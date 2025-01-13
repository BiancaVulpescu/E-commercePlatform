using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDtoMinimal>>> GetAllShoppingCarts()
        {
            return (await mediator.Send(new GetAllOrdersQuery { }))
                .Match<ActionResult<IEnumerable<OrderDtoMinimal>>>(
                    orderDtos => Ok(orderDtos),
                    error => BadRequest(error)
                );
        }
        [HttpGet("profile/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderDtoMinimal>>> GetAllOrdersByUserId([FromRoute] Guid userId)
        {
            return (await mediator.Send(new GetAllOrdersByUserIdQuery {UserId = userId }))
                .Match<ActionResult<IEnumerable<OrderDtoMinimal>>>(
                    orderDtos => Ok(orderDtos),
                    error => BadRequest(error)
                );
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetOrderById([FromRoute] Guid id)
        {
            return (await mediator.Send(new GetOrderByIdQuery { Id = id }))
                .Match<ActionResult<OrderDto>>(
                    orderDto => Ok(orderDto),
                    error => BadRequest(error)
                );
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            return (await mediator.Send(command)).Match<ActionResult<Guid>>(
                id => CreatedAtAction(nameof(CreateOrder), new { id }, id),
                error => BadRequest(error)
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command, [FromRoute] Guid id)
        {
            return command.Id == id ? (await mediator.Send(command))
                .Match<ActionResult>(
                    _ => NoContent(),
                    error => BadRequest(error)
                ) : BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteOrder([FromRoute] Guid id)
        {
            return (await mediator.Send(new DeleteOrderCommand { Id = id }))
                .Match<ActionResult>(
                    _ => NoContent(),
                    error => BadRequest(error)
                );
        }

        [HttpGet("{id:guid}/products")]
        public async Task<ActionResult<IEnumerable<OrderProductDtoP>>> GetAllProductsByOrderId([FromRoute] Guid id)
        {
            return (await mediator.Send(new GetAllProductsByOrderIdQuery { Id = id }))
                .Match<ActionResult<IEnumerable<OrderProductDtoP>>>(
                    orderProductDtos => Ok(orderProductDtos),
                    error => BadRequest(error)
                );
        }

        [HttpPut("{orderId:guid}/products/{productId:guid}")]
        public async Task<ActionResult> AddProductToOrder([FromRoute] Guid orderId, [FromRoute] Guid productId, [FromQuery] uint quantity)
        {
            var command = new AddProductToOrderCommand
            {
                ProductId = productId,
                OrderId = orderId,
                Quantity = quantity
            };
            return (await mediator.Send(command)).Match<ActionResult>(
                    _ => NoContent(),
                    error => BadRequest(error)
                );
        }

        [HttpDelete("{orderId:guid}/products/{productId:guid}")]
        public async Task<ActionResult> DeleteProductFromOrder([FromRoute] Guid orderId, [FromRoute] Guid productId)
        {
            return (await mediator.Send(new DeleteProductFromOrderCommand { ProductId = productId, OrderId = orderId }))
                .Match<ActionResult>(
                    _ => NoContent(),
                    error => BadRequest(error)
                );
        }
    }
}
