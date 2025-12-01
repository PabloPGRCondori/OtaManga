using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using OtoMangaStore.Infrastructure.Persistence;

namespace OtoMangaStore.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly OtoDbContext _db;
        public CategoryRepository(OtoDbContext db) => _db = db;

        public async Task AddAsync(Category category) => await _db.Categories.AddAsync(category);

        public async Task DeleteAsync(Category category)
        {
            _db.Categories.Remove(category);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _db.Categories.AsNoTracking().ToListAsync();

        public async Task<Category?> GetByIdAsync(int id) =>
            await _db.Categories.FindAsync(id);

        public Task UpdateAsync(Category category)
        {
            _db.Categories.Update(category);
            return Task.CompletedTask;
        }
    }
}
