using OtoMangaStore.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    public interface IMangaRepository 
    {
        Task<content> GetMangaDetailsAsync(int mangaId);
        Task<IEnumerable<content>> GetMangaByCategoryAsync(int categoryId);
        Task UpdateAsync(content manga);
        Task<content> GetByIdAsync(int mangaId);

        // Nuevos para el CRUD
        Task AddAsync(content manga);
        Task<IEnumerable<content>> GetAllAsync();
        Task DeleteAsync(content manga);
    }
}