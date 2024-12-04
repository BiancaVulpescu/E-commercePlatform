using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using Application.Use_Cases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDtoMinimal>>> GetAllProducts()
        {
            return (await mediator.Send(new GetAllProductsQuery { }))
                .Match<ActionResult<IEnumerable<ProductDtoMinimal>>>(
                    productDtos => Ok(productDtos),
                    error => BadRequest(error)
                );
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] Guid id)
        {
            return (await mediator.Send(new GetProductByIdQuery { Id = id }))
                .Match<ActionResult<ProductDto>>(
                    productDto => Ok(productDto),
                    error => BadRequest(error)
                );
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct([FromBody] CreateProductCommand command)
        {
            return (await mediator.Send(command))
                .Match<ActionResult<Guid>>(
                    id => CreatedAtAction(nameof(CreateProduct), new { id }, id),
                    error => BadRequest(error)
                );
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommand command, [FromRoute] Guid id)
        {
            return command.Id == id ? (await mediator.Send(command))
                .Match<ActionResult>(
                    _ => NoContent(),
                    error => BadRequest(error)
                ) : BadRequest();
        }
        [HttpGet("paginated")]
        public async Task<ActionResult> GetAllProductsPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            return (await mediator.Send(new GetAllProductsPaginatedQuery { Page = page, PageSize = pageSize }))
                      .Match<ActionResult>(
                          productDtos => Ok(productDtos),
                          error => BadRequest(error)
                      );
        }
        [HttpGet("by-title/{title}")]
        public async Task<IActionResult> GetProductsByTitle([FromRoute] string title, [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            return (await mediator.Send(new GetProductsByTitleQuery { Title = title, Page = page, PageSize = pageSize }))
                      .Match<ActionResult>(
                          productDtos => Ok(productDtos),
                          error => BadRequest(error)
                      );
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] Guid id)
        {
            return (await mediator.Send(new DeleteProductCommand { Id = id }))
                .Match<ActionResult>(
                    _ => NoContent(),
                    error => BadRequest(error)
                );
        }

        [HttpGet("{id:guid}/shoppingcarts")]
        public async Task<ActionResult<IEnumerable<ShoppingCartDtoMinimal>>> GetAllShoppingCartsByProductId([FromRoute] Guid id)
        {
            return (await mediator.Send(new GetAllShoppingCartsByProductIdQuery { Id = id }))
                .Match<ActionResult<IEnumerable<ShoppingCartDtoMinimal>>>(
                    shoppingCartDtos => Ok(shoppingCartDtos),
                    error => BadRequest(error)
                );
        }

        [HttpGet("{id:guid}/wishlists")]
        public async Task<ActionResult<IEnumerable<WishlistDtoMinimal>>> GetAllWishlistsByProductId([FromRoute] Guid id)
        {
            return (await mediator.Send(new GetAllShoppingCartsByProductIdQuery { Id = id }))
                .Match<ActionResult<IEnumerable<WishlistDtoMinimal>>>(
                    shoppingCartDtos => Ok(shoppingCartDtos),
                    error => BadRequest(error)
                );
        }
    }
}
