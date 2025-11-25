using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Services;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationService _service;

        public RecommendationsController(IRecommendationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MangaDto>>> Get([FromQuery] string userId, [FromQuery] int top = 10)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("userId es requerido");
            }
            if (top <= 0) top = 10;
            var data = await _service.GetRecommendationsAsync(userId, top);
            return Ok(data);
        }
    }
}
