using System;
using System.Linq;
using System.Threading.Tasks;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Application.Interfaces.Services;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> CreateOrderAsync(CreateOrderDto orderDto)
        {
            var order = new Order
            {
                ExternalUserId = orderDto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = "Created",
                TotalAmount = 0m,
                OrderItems = []
            };

            foreach (var item in orderDto.Items)
            {
                var manga = await _uow.Mangas.GetByIdAsync(item.MangaId);
                if (manga == null)
                {
                    throw new InvalidOperationException($"Manga {item.MangaId} no encontrado");
                }

                manga.DecreaseStock(item.Quantity);
                await _uow.Mangas.UpdateAsync(manga);

                var price = await _uow.PriceHistory.GetCurrentPriceAsync(manga.Id);
                var orderItem = new OrderItem
                {
                    MangaId = manga.Id,
                    Quantity = item.Quantity,
                    UnitPrice = price,
                    Content = manga
                };
                order.OrderItems.Add(orderItem);
                order.TotalAmount += price * item.Quantity;
            }

            await _uow.Orders.AddAsync(order);
            await _uow.SaveChangesAsync();
            return order.Id;
        }
    }
}