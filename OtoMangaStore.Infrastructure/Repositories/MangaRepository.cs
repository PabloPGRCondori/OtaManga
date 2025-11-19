using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using OtoMangaStore.Infrastructure.Persistence;

namespace OtoMangaStore.Infrastructure.Repositories
{
    public class MangaRepository : IMangaRepository
    {
        private readonly OtoDbContext _db;

        public MangaRepository(OtoDbContext db)
        {
            _db = db;
        }

        public async Task<content> GetByIdAsync(int mangaId)
        {
            return await _db.Mangas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == mangaId);
        }

        public async Task<content> GetMangaDetailsAsync(int id)
        {
            return await _db.Mangas
                .AsNoTracking()
                .Include(m => m.Category)
                .Include(m => m.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<content>> GetMangaByCategoryAsync(int categoryId)
        {
            return await _db.Mangas
                .AsNoTracking()
                .Include(m => m.Category)
                .Include(m => m.Author)
                .Where(m => m.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task UpdateAsync(content manga)
        {
            _db.Mangas.Update(manga);
            await _db.SaveChangesAsync();
        }
    }
}