using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    public interface IMangaRepository
    {
        Task<IEnumerable<Manga>> GetAllAsync();
        Task<Manga> GetByIdAsync(int id);
        Task UpdateStockAsync(Manga entity);
    }
}
