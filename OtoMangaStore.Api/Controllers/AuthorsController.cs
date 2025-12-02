using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using OtoMangaStore.Application.UseCases.Authors.Commands.CreateAuthor;
using OtoMangaStore.Application.UseCases.Authors.Commands.UpdateAuthor;
using OtoMangaStore.Application.UseCases.Authors.Commands.DeleteAuthor;
using OtoMangaStore.Application.UseCases.Authors.Queries.GetAllAuthors;
using OtoMangaStore.Application.UseCases.Authors.Queries.GetAuthorById;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
        {
            var authors = await _mediator.Send(new GetAllAuthorsQuery());
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery(id));
            if (author == null) return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteAuthorCommand(id));
            return Ok();
        }
    }
}
