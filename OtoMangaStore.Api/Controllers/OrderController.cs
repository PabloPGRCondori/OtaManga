using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Services;

namespace OtoMangaStore.Api.Controllers;

[ApiController]
[Route("api/orders")] // Ruta específica
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    // Inyectamos la interfaz (recibirá el OrderService REAL)
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
    {
        if (orderDto == null || !orderDto.CartItems.Any())
        {
            return BadRequest("La orden no puede estar vacía.");
        }

        try
        {
            // El OrderService REAL usará el UoW Híbrido
            await _orderService.CreateOrderAsync(orderDto);
            return Ok(new { message = "Orden creada exitosamente (simulación)." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al crear la orden: {ex.Message}");
        }
    }
}