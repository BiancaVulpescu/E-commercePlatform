using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class ShoppingCartsController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator mediator = mediator;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ShoppingCartDtoMinimal>>> GetAllShoppingCarts()
		{
			return (await mediator.Send(new GetAllShoppingCartsQuery { }))
				.Match<ActionResult<IEnumerable<ShoppingCartDtoMinimal>>>(
					shoppingCartDtos => Ok(shoppingCartDtos),
					error => BadRequest(error)
				);
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<ShoppingCartDto>> GetShoppingCartById([FromRoute] Guid id)
		{
			return (await mediator.Send(new GetShoppingCartByIdQuery { Id = id }))
				.Match<ActionResult<ShoppingCartDto>>(
					shoppingCartDto => Ok(shoppingCartDto),
					error => BadRequest(error)
				);
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> CreateShoppingCart([FromBody] CreateShoppingCartCommand command)
		{
			return (await mediator.Send(command)).Match<ActionResult<Guid>>(
				id => CreatedAtAction(nameof(CreateShoppingCart), new { id }, id),
				error => BadRequest(error)
			);
		}

		[HttpPut("{id:guid}")]
		public async Task<ActionResult> UpdateShoppingCart([FromBody] UpdateShoppingCartCommand command, [FromRoute] Guid id)
		{
			return command.Id == id ? (await mediator.Send(command))
				.Match<ActionResult>(
					_ => NoContent(),
					error => BadRequest(error)
				) : BadRequest();
		}

		[HttpDelete("{id:guid}")]
		public async Task<ActionResult> DeleteShoppingCart([FromRoute] Guid id)
		{
			return (await mediator.Send(new DeleteShoppingCartCommand { Id = id }))
				.Match<ActionResult>(
					_ => NoContent(),
					error => BadRequest(error)
				);
		}

		[HttpGet("{id:guid}/products")]
		public async Task<ActionResult<IEnumerable<ShoppingCartProductDtoP>>> GetAllProductsByShoppingCartId([FromRoute] Guid id)
		{
			return (await mediator.Send(new GetAllProductsByShoppingCartIdQuery { Id = id }))
				.Match<ActionResult<IEnumerable<ShoppingCartProductDtoP>>>(
					shoppingCartDtos => Ok(shoppingCartDtos),
					error => BadRequest(error)
				);
		}

		[HttpPut("{shoppingCartId:guid}/products/{productId:guid}")]
		public async Task<ActionResult> AddProductToShoppingCart([FromRoute] Guid shoppingCartId, [FromRoute] Guid productId, [FromQuery] uint quantity)
		{
            var command = new AddProductToShoppingCartCommand
            {
                ProductId = productId,
                ShoppingCartId = shoppingCartId,
                Quantity = quantity
            };
            return (await mediator.Send(command)).Match<ActionResult>(
					_ => NoContent(),
					error => BadRequest(error)
				);
		}

		[HttpDelete("{shoppingCartId:guid}/products/{productId:guid}")]
		public async Task<ActionResult> DeleteProductFromShoppingCart([FromRoute] Guid shoppingCartId, [FromRoute] Guid productId)
		{
			return (await mediator.Send(new DeleteProductFromShoppingCartCommand { ProductId = productId, ShoppingCartId = shoppingCartId }))
				.Match<ActionResult>(
					_ => NoContent(),
					error => BadRequest(error)
				);
		}
	}
}
