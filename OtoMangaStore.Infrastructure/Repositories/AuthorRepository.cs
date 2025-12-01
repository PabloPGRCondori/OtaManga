using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using OtoMangaStore.Infrastructure.Persistence;

namespace OtoMangaStore.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly OtoDbContext _db;
        public AuthorRepository(OtoDbContext db) => _db = db;

        public async Task AddAsync(Author author) => await _db.Authors.AddAsync(author);

        public async Task DeleteAsync(Author author)
        {
            _db.Authors.Remove(author);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Author>> GetAllAsync() =>
            await _db.Authors.AsNoTracking().ToListAsync();

        public async Task<Author?> GetByIdAsync(int id) =>
            await _db.Authors.FindAsync(id);

        public Task UpdateAsync(Author author)
        {
            _db.Authors.Update(author);
            return Task.CompletedTask;
        }
    }
}
