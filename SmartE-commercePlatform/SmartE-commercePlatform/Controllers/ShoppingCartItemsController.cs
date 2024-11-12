﻿using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingCartItemsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShoppingCartItemsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // Create ShoppingCartItem
        [HttpPost]
        public async Task<IActionResult> CreateShoppingCartItem([FromBody] CreateShoppingCartItemCommand createShoppingCartItemCommand)
        {
            var resultObject = await mediator.Send(createShoppingCartItemCommand);
            return resultObject.MapOrElse<IActionResult>(
                result => CreatedAtAction(nameof(GetShoppingCartItemById), new { id = result }, result),
                error => BadRequest(error)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingCartItemById([FromRoute] Guid id)
        {
            var resultObject = await mediator.Send(new GetShoppingCartItemByIdQuery { Id = id });
            return resultObject.MapOrElse<IActionResult>(
                result => Ok(result),
                error => BadRequest(error)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShoppingCartItems()
        {
            var result = await mediator.Send(new GetAllShoppingCartItemsQuery());
            return Ok(result);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateShoppingCartItem([FromBody] UpdateShoppingCartItemCommand updateShoppingCartItemCommand, [FromRoute] Guid id)
        {
            if (updateShoppingCartItemCommand.Id != id)
                return BadRequest("Id mismatch.");

            var resultObject = await mediator.Send(updateShoppingCartItemCommand);
            return resultObject.MapOrElse<IActionResult>(
                result => NoContent(),
                error => BadRequest(error)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCartItem([FromRoute] Guid id)
        {
            var resultObject = await mediator.Send(new DeleteShoppingCartItemCommand { Id = id });
            return resultObject.MapOrElse<IActionResult>(
                result => NoContent(),
                error => BadRequest(error)
            );
        }
    }
}
