using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.ExternalUserId == userId)
                .Include(o => o.OrderItems)
                .ToListAsync();
        }
    }
}