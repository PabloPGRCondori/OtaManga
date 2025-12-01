using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs.Orders;
using OtoMangaStore.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Controllers.Admin
{
    [Route("api/admin/orders")]
    [ApiController]
    [Authorize(Roles = "Admin,Editor")]
    public class AdminOrdersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public AdminOrdersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/admin/orders
        // Listado general para el Dashboard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminOrderDto>>> GetAllOrders()
        {
            var orders = await _uow.Orders.GetAllAsync(); 

            // Mapeo manual a tu DTO limpio (AdminOrderDto)
            var result = orders.Select(o => new AdminOrderDto
            {
                Id = o.Id,
                UserEmail = o.ExternalUserId,
                Date = o.OrderDate,
                Total = o.TotalAmount,
                Status = o.Status,
                Items = o.OrderItems?.Select(i => new AdminOrderDetailDto
                {
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList() ?? new List<AdminOrderDetailDto>()
            });
            return Ok(result.OrderByDescending(x => x.Date));
        }
    }
}