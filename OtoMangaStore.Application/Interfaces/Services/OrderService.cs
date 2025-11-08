using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.Interfaces.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CheckStockAsync(int mangaId, int quantity)
    {
        var manga = await _unitOfWork.Mangas.GetByIdAsync(mangaId);

        if (manga is null)
            return false;

        return manga.Stock >= quantity;
    }

    public async Task<decimal> CalculateTotalAsync(CreateOrderDto orderDto)
    {
        if (orderDto.CartItems is null || !orderDto.CartItems.Any())
            return 0;

        decimal total = 0;

        foreach (var item in orderDto.CartItems)
        {
            var manga = await _unitOfWork.Mangas.GetByIdAsync(item.MangaId);
            if (manga is not null)
            {
                total += manga.Price * item.Quantity;
            }
        }

        return total;
    }

    public async Task CreateOrderAsync(CreateOrderDto orderDto)
    {
        if (orderDto.CartItems is null || !orderDto.CartItems.Any())
            return;

        foreach (var item in orderDto.CartItems)
        {
            var manga = await _unitOfWork.Mangas.GetByIdAsync(item.MangaId);

            if (manga is null)
                continue;

            if (manga.Stock < item.Quantity)
                continue;

            manga.Stock -= item.Quantity;
            
            await _unitOfWork.Mangas.UpdateStockAsync(manga);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}