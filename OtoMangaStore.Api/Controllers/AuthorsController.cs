using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using System.Linq;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly MediatR.IMediator _mediator;

        public AuthorsController(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
        {
            var authors = await _mediator.Send(new OtoMangaStore.Application.UseCases.Authors.Queries.GetAllAuthors.GetAllAuthorsQuery());
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var author = await _mediator.Send(new OtoMangaStore.Application.UseCases.Authors.Queries.GetAuthorById.GetAuthorByIdQuery(id));
            if (author == null) return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OtoMangaStore.Api.DTOs.Requests.CreateAuthorRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new OtoMangaStore.Application.UseCases.Authors.Commands.CreateAuthor.CreateAuthorCommand
            {
                Name = request.Name,
                Description = request.Description
            };

            var id = await _mediator.Send(command); // Assuming Mediator is injected, but AuthorsController uses _uow directly!

            // Wait, AuthorsController uses _uow directly. I need to refactor it to use Mediator!
            // The previous refactoring was for Admin Razor Pages, not the API Controller.
            // But to use the new Commands (which I refactored), I MUST use Mediator here.
            
            return Ok(id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OtoMangaStore.Api.DTOs.Requests.UpdateAuthorRequest request)
        {
            if (id != request.Id) return BadRequest("ID mismatch");
            
            var command = new OtoMangaStore.Application.UseCases.Authors.Commands.UpdateAuthor.UpdateAuthorCommand
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new OtoMangaStore.Application.UseCases.Authors.Commands.DeleteAuthor.DeleteAuthorCommand(id));
            return Ok();
        }
    }
}
