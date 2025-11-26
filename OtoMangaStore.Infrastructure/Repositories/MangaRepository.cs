using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<content> GetMangaDetailsAsync(int mangaId)
        {
            return await _context.Mangas
                .Include(m => m.Category)
                .Include(m => m.Author)
                .FirstOrDefaultAsync(m => m.Id == mangaId);
        }

        public async Task<IEnumerable<content>> GetMangaByCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                // devolver todo si categoryId <= 0
                return await _context.Mangas
                    .Include(m => m.Category)
                    .Include(m => m.Author)
                    .ToListAsync();
            }

            return await _context.Mangas
                .Where(m => m.CategoryId == categoryId)
                .Include(m => m.Category)
                .Include(m => m.Author)
                .ToListAsync();
        }

        public Task UpdateAsync(content manga)
        {
            _context.Mangas.Update(manga);
            return Task.CompletedTask;
        }

        public async Task<content> GetByIdAsync(int mangaId)
        {
            return await _context.Mangas.FindAsync(mangaId);
        }

        // Nuevos métodos
        public async Task AddAsync(content manga)
        {
            await _context.Mangas.AddAsync(manga);
        }

        public async Task<IEnumerable<content>> GetAllAsync()
        {
            return await _context.Mangas
                .Include(m => m.Category)
                .Include(m => m.Author)
                .ToListAsync();
        }

        public Task DeleteAsync(content manga)
        {
            _context.Mangas.Remove(manga);
            return Task.CompletedTask;
        }
    }
}
