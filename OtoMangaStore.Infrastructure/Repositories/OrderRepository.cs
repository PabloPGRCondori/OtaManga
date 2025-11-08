using System.Threading.Tasks;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using OtoMangaStore.Infrastructure.Persistence;

namespace OtoMangaStore.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OtoDbContext _context;

        public OrderRepository(OtoDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            // Nota: El guardado de cambios (SaveChangesAsync) se delega,
            // usualmente a una Unidad de Trabajo (Unit of Work).
        }
    }
}
