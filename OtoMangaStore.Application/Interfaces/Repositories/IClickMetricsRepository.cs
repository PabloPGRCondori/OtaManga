using System.Collections.Generic;
using System.Threading.Tasks;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    public interface IClickMetricsRepository
    {
        Task RegisterClickAsync(int mangaId, string userId);
        Task<IReadOnlyList<ClickTopDto>> GetTopClickedAsync(int top);
        Task<IReadOnlyList<CategoryRankingDto>> GetCategoryRankingAsync();
    }
}
