using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Application.Interfaces.Services;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangasController : ControllerBase
    {
        private readonly IMangaService _mangaService;

        public MangasController(IMangaService mangaService)
        {
            _mangaService = mangaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MangaDto>>> GetByCategory([FromQuery] int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest("categoryId es requerido y debe ser mayor a 0");
            }

            var result = await _mangaService.GetMangasByCategoryAsync(categoryId);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MangaDto>> GetById(int id)
        {
            var dto = await _mangaService.GetMangaByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
    }
}
