using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<bool> CheckStockAsync(int mangaId, int quantity);
        Task<decimal> CalculateTotalAsync(CreateOrderDto orderDto);
        Task CreateOrderAsync(CreateOrderDto orderDto);
    }
}