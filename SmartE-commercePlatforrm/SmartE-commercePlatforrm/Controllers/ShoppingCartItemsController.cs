﻿using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatforrm.Controllers
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
            return resultObject.Match<IActionResult>(
                onSuccess: result => CreatedAtAction(nameof(GetShoppingCartItemById), new { id = result }, result),
                onFailure: error => BadRequest(error)
            );
        }

        // Get ShoppingCartItem by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingCartItemById([FromRoute] Guid id)
        {
            var resultObject = await mediator.Send(new GetShoppingCartItemByIdQuery { Id = id });
            return resultObject.Match<IActionResult>(
                onSuccess: result => Ok(result),
                onFailure: error => BadRequest(error)
            );
        }

        // Get all items in the cart by CartId
        [HttpGet("cart/{cartId}")]
        public async Task<IActionResult> GetAllShoppingCartItems([FromRoute] Guid cartId)
        {
            var resultObject = await mediator.Send(new GetAllShoppingCartItemsQuery { CartId = cartId });
            return resultObject.Match<IActionResult>(
                onSuccess: result => Ok(result),
                onFailure: error => BadRequest(error)
            );
        }

        // Update ShoppingCartItem
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateShoppingCartItem([FromBody] UpdateShoppingCartItemCommand updateShoppingCartItemCommand, [FromRoute] Guid id)
        {
            if (updateShoppingCartItemCommand.Id != id)
                return BadRequest("Id mismatch.");

            var resultObject = await mediator.Send(updateShoppingCartItemCommand);
            return resultObject.Match<IActionResult>(
                onSuccess: result => NoContent(),
                onFailure: error => BadRequest(error)
            );
        }

        // Delete ShoppingCartItem
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCartItem([FromRoute] Guid id)
        {
            var resultObject = await mediator.Send(new DeleteShoppingCartItemCommand { Id = id });
            return resultObject.Match<IActionResult>(
                onSuccess: result => NoContent(),
                onFailure: error => BadRequest(error)
            );
        }
    }
}