using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs.PriceHistory;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace OtoMangaStore.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Editor")]
    public class PriceHistoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public PriceHistoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{contentId}")]
        public async Task<IActionResult> GetHistoryByContent(int contentId)
        {
            var history = await _uow.PriceHistory.GetHistoryByMangaIdAsync(contentId);
            
            return Ok(history);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPrice([FromBody] CreatePriceHistoryDto dto)
        {
            var contentItem = await _uow.Mangas.GetByIdAsync(dto.ContentId);
            if (contentItem == null) 
                return NotFound($"No existe contenido con ID {dto.ContentId}");

            var newPrice = new PriceHistory
            {
                MangaId = dto.ContentId,
                Price = dto.NewPrice,
                EffectiveDate = DateTime.UtcNow
            };

            await _uow.PriceHistory.AddAsync(newPrice);
            
            await _uow.SaveChangesAsync(); 

            return Ok(new { message = "Precio actualizado correctamente", newPrice = dto.NewPrice });
        }
    }
}