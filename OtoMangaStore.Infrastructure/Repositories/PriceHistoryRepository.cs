using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Infrastructure.Persistence;

namespace OtoMangaStore.Infrastructure.Repositories
{
    public class PriceHistoryRepository : IPriceHistoryRepository
    {
        private readonly OtoDbContext _db;

        public PriceHistoryRepository(OtoDbContext db)
        {
            _db = db;
        }

        public async Task<decimal> GetCurrentPriceAsync(int mangaId)
        {
            var entry = await _db.PriceHistories
                .AsNoTracking()
                .Where(p => p.MangaId == mangaId)
                .OrderByDescending(p => p.EffectiveDate)
                .FirstOrDefaultAsync();

            return entry?.Price ?? 0m;
        }
    }
}
