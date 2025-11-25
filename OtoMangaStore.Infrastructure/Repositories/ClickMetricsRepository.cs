using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Infrastructure.Persistence;

namespace OtoMangaStore.Infrastructure.Repositories
{
    public class ClickMetricsRepository : IClickMetricsRepository
    {
        private readonly OtoDbContext _db;

        public ClickMetricsRepository(OtoDbContext db)
        {
            _db = db;
        }

        public async Task RegisterClickAsync(int mangaId, string userId)
        {
            _db.ClickMetrics.Add(new OtoMangaStore.Domain.Models.ClickMetric
            {
                MangaId = mangaId,
                UserId = userId,
                ClickDate = System.DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<ClickTopDto>> GetTopClickedAsync(int top)
        {
            var q = await _db.ClickMetrics
                .AsNoTracking()
                .GroupBy(x => x.MangaId)
                .Select(g => new ClickTopDto { MangaId = g.Key, Clicks = g.Count() })
                .OrderByDescending(x => x.Clicks)
                .Take(top)
                .ToListAsync();
            return q;
        }

        public async Task<IReadOnlyList<CategoryRankingDto>> GetCategoryRankingAsync()
        {
            var q = await _db.ClickMetrics
                .AsNoTracking()
                .Join(_db.Mangas,
                    cm => cm.MangaId,
                    m => m.Id,
                    (cm, m) => new { m.CategoryId })
                .GroupBy(x => x.CategoryId)
                .Select(g => new CategoryRankingDto { CategoryId = g.Key, Clicks = g.Count() })
                .OrderByDescending(x => x.Clicks)
                .ToListAsync();
            return q;
        }
    }
}
