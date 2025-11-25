using System.Collections.Generic;
using System.Threading.Tasks;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Application.Interfaces.Services;

namespace OtoMangaStore.Application.Services
{
    public class ClickMetricsService : IClickMetricsService
    {
        private readonly IClickMetricsRepository _repo;

        public ClickMetricsService(IClickMetricsRepository repo)
        {
            _repo = repo;
        }

        public Task RegisterClickAsync(int mangaId, string userId)
        {
            return _repo.RegisterClickAsync(mangaId, userId);
        }

        public Task<IReadOnlyList<ClickTopDto>> GetTopClickedAsync(int top)
        {
            return _repo.GetTopClickedAsync(top);
        }

        public Task<IReadOnlyList<CategoryRankingDto>> GetCategoryRankingAsync()
        {
            return _repo.GetCategoryRankingAsync();
        }
    }
}
