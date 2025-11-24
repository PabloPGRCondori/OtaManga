using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Application.Interfaces.Services;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _uow;

        public OrdersController(IOrderService orderService, IUnitOfWork uow)
        {
            _orderService = orderService;
            _uow = uow;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateOrderDto dto)
        {
            var id = await _orderService.CreateOrderAsync(dto);
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