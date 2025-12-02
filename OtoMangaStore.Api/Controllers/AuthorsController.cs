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
        private readonly IUnitOfWork _uow;

        public AuthorsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
        {
            var authors = await _uow.Authors.GetAllAsync();

            var dto = authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description
            });

            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var a = await _uow.Authors.GetByIdAsync(id);
            if (a == null) return NotFound();

            return Ok(new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var author = new Author
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _uow.Authors.AddAsync(author);
            await _uow.SaveChangesAsync();

            return Ok(author.Id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorDto dto)
        {
            var author = await _uow.Authors.GetByIdAsync(id);
            if (author == null) return NotFound();

            author.Name = dto.Name;
            author.Description = dto.Description;

            await _uow.Authors.UpdateAsync(author);
            await _uow.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _uow.Authors.GetByIdAsync(id);
            if (author == null) return NotFound();

            await _uow.Authors.DeleteAsync(author);
            await _uow.SaveChangesAsync();

            return Ok();
        }
    }
}
