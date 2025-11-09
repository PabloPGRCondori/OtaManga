using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
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

        public async Task<Manga> GetByIdAsync(int id)
        {
            return await _context.Mangas.FindAsync(id);
        }

        public async Task UpdateStockAsync(Manga entity)
        {
            _context.Mangas.Update(entity);
            //await _context.SaveChangesAsync();
        }
    }
}
