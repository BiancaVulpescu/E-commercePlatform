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

        //[HttpPost]
        //public async Task<IActionResult> CreateProduct(CreateProductCommand createProductCommand)
        //{
        //    var resultObject = await mediator.Send(createProductCommand);
        //    return resultObject.Match<IActionResult>(
        //        onSuccess: result => CreatedAtAction(nameof(GetProductById), new { id = result }, result),
        //        onFailure: error => BadRequest(error)
        //    );
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        //{
        //    var resultObject = await mediator.Send(new GetProductByIdQuery { Id = id });
        //    return resultObject.Match<IActionResult>(
        //        onSuccess: result => Ok(result),
        //        onFailure: error => BadRequest(error)
        //    );
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAllProducts()
        //{
        //    var result = await mediator.Send(new GetAllProductsQuery());
        //    return Ok(result);
        //}

        //[HttpPut("{id:guid}")]
        //public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand, [FromRoute] Guid id)
        //{
        //    if (updateProductCommand.Id != id)
        //        return BadRequest();
        //    var resultObject = await mediator.Send(updateProductCommand);
        //    return resultObject.Match<IActionResult>(
        //        onSuccess: result => NoContent(),
        //        onFailure: error => BadRequest(error)
        //    );
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        //{
        //    var resultObject = await mediator.Send(new DeleteProductCommand { Id = id });
        //    return resultObject.Match<IActionResult>(
        //        onSuccess: result => NoContent(),
        //        onFailure: error => BadRequest(error)
        //    );
        //}
    }
}
