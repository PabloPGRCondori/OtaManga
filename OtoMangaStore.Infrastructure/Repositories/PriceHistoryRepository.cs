using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Infrastructure.Persistence;

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
            var latest = await _context.PriceHistories
                .Where(p => p.MangaId == mangaId)
                .OrderByDescending(p => p.EffectiveDate)
                .FirstOrDefaultAsync();

            return latest?.Price ?? 0m;
        }
    }
}