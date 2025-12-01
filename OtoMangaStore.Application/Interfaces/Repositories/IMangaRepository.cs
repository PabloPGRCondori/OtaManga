using OtoMangaStore.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    public interface IMangaRepository 
    {
        Task<Content?> GetMangaDetailsAsync(int mangaId);
        Task<IEnumerable<Content>> GetMangaByCategoryAsync(int categoryId);
        Task UpdateAsync(Content manga);
        Task<Content?> GetByIdAsync(int mangaId);

        // Nuevos para el CRUD
        Task AddAsync(Content manga);
        Task<IEnumerable<Content>> GetAllAsync();
        Task DeleteAsync(Content manga);
    }
}
