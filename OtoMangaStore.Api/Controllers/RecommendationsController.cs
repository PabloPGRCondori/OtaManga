using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using MediatR;
using OtoMangaStore.Application.UseCases.Recommendations.Queries.GetRecommendations;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecommendationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MangaDto>>> Get([FromQuery] string userId, [FromQuery] int top = 10)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("userId es requerido");
            }
            if (top <= 0) top = 10;
            var data = await _mediator.Send(new GetRecommendationsQuery(userId, top));
            return Ok(data);
        }
    }
}
