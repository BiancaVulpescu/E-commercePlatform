using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using Application.Use_Cases.Queries.ProductsQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand createProductCommand)
        {
            try
            {
                var resultObject = await mediator.Send(createProductCommand);
                return resultObject.MapOrElse<IActionResult>(
                    onSuccess: result => CreatedAtAction(nameof(GetProductById), new { id = result }, result),
                    onFailure: error => BadRequest(error)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            var resultObject = await mediator.Send(new GetProductByIdQuery { Id = id });
            return resultObject.MapOrElse<IActionResult>(
                onSuccess: result => Ok(result),
                onFailure: error => BadRequest(error)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetAllProductsPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
          var resultObject = await mediator.Send(new GetAllProductsPaginatedQuery { Page = page, PageSize = pageSize });
          return resultObject.MapOrElse<IActionResult>(
              onSuccess: result => Ok(result),
              onFailure: error => BadRequest(error)
          );
        }
        [HttpGet("by-title/{title}")]
        public async Task<IActionResult> GetProductsByTitle([FromRoute] string title, [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
          var resultObject = await mediator.Send(new GetProductsByTitleQuery { Title = title, Page = page, PageSize = pageSize });
          return resultObject.MapOrElse<IActionResult>(
                onSuccess: result => Ok(result),
                onFailure: error => BadRequest(error)
          );
        }
        [HttpGet("by-category/{category}")]
        public async Task<IActionResult> GetProductsByCategory([FromRoute] string category, [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
          var resultObject = await mediator.Send(new GetProductsByCategoryQuery { Category = category, Page = page, PageSize = pageSize });
          return resultObject.MapOrElse<IActionResult>(
                onSuccess: result => Ok(result),
                onFailure: error => BadRequest(error)
          );
        }

    [HttpPut("{id:guid}")]
            public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand, [FromRoute] Guid id)
            {
                if (updateProductCommand.Id != id)
                    return BadRequest();
                try
                {
                    var resultObject = await mediator.Send(updateProductCommand);
                    return resultObject.MapOrElse<IActionResult>(
                        onSuccess: result => NoContent(),
                        onFailure: error => BadRequest(error)
                    );
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var resultObject = await mediator.Send(new DeleteProductCommand { Id = id });
            return resultObject.MapOrElse<IActionResult>(
                onSuccess: result => NoContent(),
                onFailure: error => BadRequest(error)
            );
        }
    }
}
