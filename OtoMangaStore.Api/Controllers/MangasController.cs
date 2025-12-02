using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.UseCases.Mangas.Queries.GetMangasByCategory;
using OtoMangaStore.Application.UseCases.Mangas.Queries.GetMangaById;
using OtoMangaStore.Application.UseCases.Mangas.Queries.GetAllMangas;

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
    }
}
