using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Services;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly IClickMetricsService _service;

        public MetricsController(IClickMetricsService service)
        {
            _service = service;
        }

        public class ClickInput
        {
            public int MangaId { get; set; }
            public string UserId { get; set; }
        }

        [HttpPost("click")]
        public async Task<IActionResult> RegisterClick([FromBody] ClickInput input)
        {
            if (input == null || input.MangaId <= 0 || string.IsNullOrWhiteSpace(input.UserId))
            {
                return BadRequest();
            }
            await _service.RegisterClickAsync(input.MangaId, input.UserId);
            return Ok();
        }

        [HttpGet("top")]
        public async Task<ActionResult<IReadOnlyList<ClickTopDto>>> Top([FromQuery] int top = 10)
        {
            if (top <= 0) top = 10;
            var data = await _service.GetTopClickedAsync(top);
            return Ok(data);
        }

        [HttpGet("category-ranking")]
        public async Task<ActionResult<IReadOnlyList<CategoryRankingDto>>> CategoryRanking()
        {
            var data = await _service.GetCategoryRankingAsync();
            return Ok(data);
        }
    }
}
