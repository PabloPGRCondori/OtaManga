using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.UseCases.Mangas.Queries.GetMangasByCategory;
using OtoMangaStore.Application.UseCases.Mangas.Queries.GetMangaById;
using OtoMangaStore.Application.UseCases.Mangas.Queries.GetAllMangas;
using OtoMangaStore.Application.UseCases.Mangas.Commands.CreateManga;
using OtoMangaStore.Application.UseCases.Mangas.Commands.UpdateManga;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MangasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MangaDto>>> GetAllMangas()
        {
            var mangas = await _mediator.Send(new GetAllMangasQuery());
            return Ok(mangas);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<MangaDto>>> GetMangasByCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest("El CategoryId debe ser mayor a 0.");
            }

            var mangas = await _mediator.Send(new GetMangasByCategoryQuery(categoryId));
            return Ok(mangas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MangaDto>> GetMangaById(int id)
        {
            var manga = await _mediator.Send(new GetMangaByIdQuery(id));

            if (manga == null)
            {
                return NotFound();
            }

            return Ok(manga);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OtoMangaStore.Api.DTOs.Requests.CreateMangaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateMangaCommand
            {
                Title = request.Title,
                Description = request.Description,
                AuthorId = request.AuthorId,
                CategoryId = request.CategoryId,
                Price = request.Price,
                CoverImageUrl = request.CoverImageUrl,
                Stock = request.Stock
            };

            var mangaDto = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetMangaById), new { id = mangaDto.Id }, mangaDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OtoMangaStore.Api.DTOs.Requests.UpdateMangaRequest request)
        {
            if (id != request.Id)
                return BadRequest("El ID no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new UpdateMangaCommand
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                AuthorId = request.AuthorId,
                CategoryId = request.CategoryId,
                Price = request.Price,
                CoverImageUrl = request.CoverImageUrl,
                Stock = request.Stock
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
