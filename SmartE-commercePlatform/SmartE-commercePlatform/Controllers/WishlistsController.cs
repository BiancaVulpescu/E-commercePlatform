using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WishlistsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishlistDtoMinimal>>> GetAllWishlists()
        {
            return (await mediator.Send(new GetAllWishlistsQuery { }))
                .Match<ActionResult<IEnumerable<WishlistDtoMinimal>>>(
                    wishlistDtos => Ok(wishlistDtos),
                    error => BadRequest(error)
                );
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<WishlistDto>> GetWishlistById([FromRoute] Guid id)
        {
            var resultObject = await mediator.Send(new GetWishlistByIdQuery { Id = id });
            return resultObject.Match<ActionResult<WishlistDto>>(
                result => Ok(result),
                error => BadRequest(error)
            );
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateWishlist(CreateWishlistCommand command)
        {
            return (await mediator.Send(command)).Match<ActionResult<Guid>>(
                result => CreatedAtAction(nameof(CreateWishlist), new { id = result }, result),
                error => BadRequest(error)
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateWishlist([FromBody] UpdateWishlistCommand command, [FromRoute] Guid id)
        {
            return command.Id == id ? (await mediator.Send(command))
                .Match<ActionResult>(
                    result => NoContent(),
                    error => BadRequest(error)
                ) : BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteWishlist([FromRoute] Guid id)
        {
            return (await mediator.Send(new DeleteWishlistCommand { Id = id }))
                .Match<ActionResult>(
                    _ => NoContent(),
                    error => BadRequest(error)
                );
        }

        [HttpGet("{id:guid}/products")]
        public async Task<ActionResult<IEnumerable<ProductDtoMinimal>>> GetAllProductsByWishlistId([FromRoute] Guid id)
        {
            return (await mediator.Send(new GetAllProductsByWishlistIdQuery { Id = id }))
                .Match<ActionResult<IEnumerable<ProductDtoMinimal>>>(
                    wishlistDtos => Ok(wishlistDtos),
                    error => BadRequest(error)
                );
        }

        [HttpPut("{wishlistId:guid}/products/{productId:guid}")]
        public async Task<ActionResult> AddProductToWishlist([FromRoute] Guid wishlistId, [FromRoute] Guid productId)
        {
            return (await mediator.Send(new AddProductToWishlistCommand { ProductId = productId, WishlistId = wishlistId }))
                .Match<ActionResult>(
                    _ => NoContent(),
                    error => BadRequest(error)
                );
        }

        [HttpDelete("{wishlistId:guid}/products/{productId:guid}")]
        public async Task<ActionResult> DeleteProductFromWishlist([FromRoute] Guid wishlistId, [FromRoute] Guid productId)
        {
            return (await mediator.Send(new DeleteProductFromWishlistCommand { ProductId = productId, WishlistId = wishlistId }))
                .Match<ActionResult>(
                    _ => NoContent(),
                    error => BadRequest(error)
                );
        }
    }
}
