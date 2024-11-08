using Application;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatforrm.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WishlistItemsController : ControllerBase
    {
        private readonly IMediator mediator;
        public WishlistItemsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWishlistItem(CreateWishlistItemCommand createWishlistItemCommand)
        {
            var resultObject = await mediator.Send(createWishlistItemCommand);
            return resultObject.Match<IActionResult>(
                onSuccess: result => CreatedAtAction(nameof(GetWishlistItemById), new { id = result }, result),
                onFailure: error => BadRequest(error)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWishlistItemById([FromRoute] Guid id)
        {
            var resultObject = await mediator.Send(new GetWishlistItemByIdQuery { Id = id });
            return resultObject.Match<IActionResult>(
                onSuccess: result => Ok(result),
                onFailure: error => BadRequest(error)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWishlistItems()
        {
            var result = await mediator.Send(new GetAllWishlistItemsQuery());
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateWishlistItem([FromBody] UpdateWishlistItemCommand updateWishlistItemCommand, [FromRoute] Guid id)
        {
            if (updateWishlistItemCommand.Id != id)
                return BadRequest();
            var resultObject = await mediator.Send(updateWishlistItemCommand);
            return resultObject.Match<IActionResult>(
                onSuccess: result => NoContent(),
                onFailure: error => BadRequest(error)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishlistItem([FromRoute] Guid id)
        {
            var resultObject = await mediator.Send(new DeleteWishlistItemCommand { Id = id });
            return resultObject.Match<IActionResult>(
                onSuccess: result => NoContent(),
                onFailure: error => BadRequest(error)
            );
        }
    }
}
