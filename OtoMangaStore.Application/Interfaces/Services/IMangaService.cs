using System.Collections.Generic;
using System.Threading.Tasks;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.DTOs.Mangas;

namespace OtoMangaStore.Application.Interfaces.Services
{
    public interface IMangaService
    {
        Task<IEnumerable<MangaDto>> GetMangasByCategoryAsync(int categoryId);
        Task<MangaDto?> GetMangaByIdAsync(int id);
        
        Task<MangaDto> CreateMangaAsync(CreateMangaDto dto);
        Task UpdateMangaAsync(UpdateMangaDto dto);
        Task DeleteMangaAsync(int id);
    }
}
