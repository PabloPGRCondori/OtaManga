using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using MediatR;
using OtoMangaStore.Application.UseCases.Orders.Commands.CreateOrder;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _uow;

        public OrdersController(IMediator mediator, IUnitOfWork uow)
        {
            _mediator = mediator;
            _uow = uow;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateOrderDto dto)
        {
            var id = await _mediator.Send(new CreateOrderCommand(dto));
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetByUser([FromQuery] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("userId es requerido");
            }
            var orders = await _uow.Orders.GetByUserIdAsync(userId);
            return Ok(orders);
        }
    }
}