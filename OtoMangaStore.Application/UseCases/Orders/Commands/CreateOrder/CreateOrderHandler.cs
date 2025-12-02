using MediatR;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OtoMangaStore.Application.UseCases.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly OtoMangaStore.Application.Interfaces.Repositories.IUnitOfWork _uow;

        public CreateOrderHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                ExternalUserId = request.ExternalUserId,
                OrderDate = DateTime.UtcNow,
                Status = "Created",
                TotalAmount = 0m,
                OrderItems = []
            };

            foreach (var item in request.Items)
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
