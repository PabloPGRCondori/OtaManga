using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OtoMangaStore.Application.UseCases.Orders.Commands.CreateOrder;
using OtoMangaStore.Application.UseCases.Orders.Queries.GetOrdersByUser;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] OtoMangaStore.Api.DTOs.Requests.CreateOrderRequest request)
        {
            var command = new CreateOrderCommand
            {
                ExternalUserId = request.UserId,
                Items = request.Items.Select(i => new OrderItemCommand
                {
                    MangaId = i.MangaId,
                    Quantity = i.Quantity
                }).ToList()
            };

            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetByUser([FromQuery] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("userId es requerido");
            }
            
            var orders = await _mediator.Send(new GetOrdersByUserQuery(userId));
            return Ok(orders);
        }
    }
}