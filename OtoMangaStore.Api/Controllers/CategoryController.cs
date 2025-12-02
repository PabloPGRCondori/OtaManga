using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MediatR.IMediator _mediator;

        public CategoryController(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new OtoMangaStore.Application.UseCases.Categories.Queries.GetAllCategories.GetAllCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _mediator.Send(new OtoMangaStore.Application.UseCases.Categories.Queries.GetCategoryById.GetCategoryByIdQuery(id));
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OtoMangaStore.Api.DTOs.Requests.CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new OtoMangaStore.Application.UseCases.Categories.Commands.CreateCategory.CreateCategoryCommand
            {
                Name = request.Name
            };

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, new { id = id, name = request.Name });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OtoMangaStore.Api.DTOs.Requests.UpdateCategoryRequest request)
        {
            if (id != request.Id)
                return BadRequest("El ID no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new OtoMangaStore.Application.UseCases.Categories.Commands.UpdateCategory.UpdateCategoryCommand
            {
                Id = request.Id,
                Name = request.Name
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new OtoMangaStore.Application.UseCases.Categories.Commands.DeleteCategory.DeleteCategoryCommand(id));
            return NoContent();
        }
    }
}
