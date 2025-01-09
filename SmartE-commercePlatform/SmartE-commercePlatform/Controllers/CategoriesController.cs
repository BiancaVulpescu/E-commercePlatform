using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CategoryDtoMinimal>>> GetAllCategories()
        {
            return (await mediator.Send(new GetAllCategoriesQuery { }))
                .Match<ActionResult<IEnumerable<CategoryDtoMinimal>>>(
                    categoryDtos => Ok(categoryDtos),
                    error => BadRequest(error)
                );
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<CategoryDto>> GetCategoryById([FromRoute] Guid id)
        {
            return (await mediator.Send(new GetProductByIdQuery { Id = id }))
                .Match<ActionResult<CategoryDto>>(
                    categoryDto => Ok(categoryDto),
                    error => BadRequest(error)
                );
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            return (await mediator.Send(command))
                .Match<ActionResult<Guid>>(
                    id => CreatedAtAction(nameof(CreateCategory), new { id }, id),
                    error => BadRequest(error)
                );
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command, [FromRoute] Guid id)
        {
            return command.Id == id ? (await mediator.Send(command))
                .Match<ActionResult>(
                    _ => NoContent(),
                    error => BadRequest(error)
                ) : BadRequest();
        }
        [HttpGet("by-title/{title}")]
        [Authorize]
        public async Task<IActionResult> GetCategoriesByTitle([FromRoute] string title)
        {
            return (await mediator.Send(new GetCategoriesByTitleQuery { Title = title }))
                      .Match<ActionResult>(
                          categoryDtos => Ok(categoryDtos),
                          error => BadRequest(error)
                      );
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> DeleteCategory([FromRoute] Guid id)
        {
            return (await mediator.Send(new DeleteCategoryCommand { Id = id }))
                .Match<ActionResult>(
                    _ => NoContent(),
                    error => BadRequest(error)
                );
        }
    }
}
