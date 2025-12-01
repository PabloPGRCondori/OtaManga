using System.Collections.Generic;
using System.Threading.Tasks;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(Author author);
    }
}
