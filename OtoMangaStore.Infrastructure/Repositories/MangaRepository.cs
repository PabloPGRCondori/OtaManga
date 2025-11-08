using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Entities;
using OtoMangaStore.Infrastructure.Persistence;

namespace OtoMangaStore.Infrastructure.Repositories
{
    public class MangaRepository : IMangaRepository
    {
        private readonly OtoDbContext _context;

        public MangaRepository(OtoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manga>> GetAllAsync()
        {
            return await _context.Mangas.ToListAsync();
        }

        public async Task<Manga> GetByIdAsync(Guid id)
        {
            return await _context.Mangas.FindAsync(id);
        }

        public async Task UpdateStockAsync(Guid mangaId, int quantity)
        {
            var manga = await _context.Mangas.FindAsync(mangaId);
            if (manga != null)
            {
                manga.Stock -= quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
