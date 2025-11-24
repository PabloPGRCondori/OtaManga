using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangasController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public MangasController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MangaDto>>> GetByCategory([FromQuery] int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest("categoryId es requerido y debe ser mayor a 0");
            }

            var items = await _uow.Mangas.GetMangaByCategoryAsync(categoryId);
            var list = items.ToList();

            var prices = await Task.WhenAll(list.Select(m => _uow.PriceHistory.GetCurrentPriceAsync(m.Id)));

            var result = list.Select((m, idx) => new MangaDto
            {
                Id = m.Id,
                Title = m.Title,
                Stock = m.Stock,
                Synopsis = m.Synopsis,
                ImageUrl = m.ImageUrl,
                CategoryId = m.CategoryId,
                CategoryName = m.Category?.Name,
                AuthorId = m.AuthorId,
                AuthorName = m.Author?.Name,
                CurrentPrice = prices[idx]
            });

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MangaDto>> GetById(int id)
        {
            var m = await _uow.Mangas.GetMangaDetailsAsync(id);
            if (m == null)
            {
                return NotFound();
            }

            var price = await _uow.PriceHistory.GetCurrentPriceAsync(m.Id);
            var dto = new MangaDto
            {
                Id = m.Id,
                Title = m.Title,
                Stock = m.Stock,
                Synopsis = m.Synopsis,
                ImageUrl = m.ImageUrl,
                CategoryId = m.CategoryId,
                CategoryName = m.Category?.Name,
                AuthorId = m.AuthorId,
                AuthorName = m.Author?.Name,
                CurrentPrice = price
            };
            return Ok(dto);
        }
    }
}