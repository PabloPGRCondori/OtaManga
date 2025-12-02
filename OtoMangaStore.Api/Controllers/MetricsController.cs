using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using MediatR;
using OtoMangaStore.Application.UseCases.Metrics.Commands.RegisterClick;
using OtoMangaStore.Application.UseCases.Metrics.Queries.GetTopClicked;
using OtoMangaStore.Application.UseCases.Metrics.Queries.GetCategoryRanking;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MetricsController(IMediator mediator)
        {
            _mediator = mediator;
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
            await _mediator.Send(new RegisterClickCommand(input.MangaId, input.UserId));
            return Ok();
        }

        [HttpGet("top")]
        public async Task<ActionResult<IReadOnlyList<ClickTopDto>>> Top([FromQuery] int top = 10)
        {
            if (top <= 0) top = 10;
            var data = await _mediator.Send(new GetTopClickedQuery(top));
            return Ok(data);
        }

        [HttpGet("category-ranking")]
        public async Task<ActionResult<IReadOnlyList<CategoryRankingDto>>> CategoryRanking()
        {
            var data = await _mediator.Send(new GetCategoryRankingQuery());
            return Ok(data);
        }
    }
}
