using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Infrastructure.Persistence;
using System.Collections.Generic;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Infrastructure.Repositories
{
    public class PriceHistoryRepository : IPriceHistoryRepository
    {
        private readonly OtoDbContext _context;

        public PriceHistoryRepository(OtoDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetCurrentPriceAsync(int mangaId)
        {
            // Busca el precio más reciente, o retorna 0 si no hay historial
            var history = await _context.PriceHistories
                .Where(ph => ph.MangaId == mangaId)
                .OrderByDescending(ph => ph.EffectiveDate)
                .FirstOrDefaultAsync();

            return history?.Price ?? 0m;
        }
        
        public async Task AddAsync(PriceHistory priceHistory)
        {
            await _context.PriceHistories.AddAsync(priceHistory);
        }

        public async Task<IEnumerable<PriceHistory>> GetHistoryByMangaIdAsync(int mangaId)
        {
            return await _context.PriceHistories
                .Where(ph => ph.MangaId == mangaId)
                .OrderByDescending(ph => ph.EffectiveDate) // Ordenado por fecha
                .ToListAsync();
        }
    }
}