using OtoMangaStore.Application.DTOs;
using System.Threading.Tasks;

namespace OtoMangaStore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        // El servicio procesa la creación de la orden, llama a DecreaseStock en Manga.cs,
        // y registra el precio actual en PriceHistory.
        Task<int> CreateOrderAsync(CreateOrderDto orderDto);
        
        // ... otros métodos
    }
}