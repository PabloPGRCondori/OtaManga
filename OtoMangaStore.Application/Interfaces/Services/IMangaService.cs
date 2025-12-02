using System.Collections.Generic;
using System.Threading.Tasks;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.Interfaces.Services
{
    public interface IMangaService
    {
        Task<IEnumerable<MangaDto>> GetMangasByCategoryAsync(int categoryId);
        Task<MangaDto?> GetMangaByIdAsync(int id);
    }
}
