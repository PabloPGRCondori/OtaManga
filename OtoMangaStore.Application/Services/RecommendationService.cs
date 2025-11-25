using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Application.Interfaces.Services;

namespace OtoMangaStore.Application.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IUnitOfWork _uow;

        public RecommendationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<MangaDto>> GetRecommendationsAsync(string userId, int top)
        {
            var ranking = await _uow.ClickMetrics.GetCategoryRankingAsync();
            var favorite = ranking.OrderByDescending(x => x.Clicks).FirstOrDefault();
            var categoryId = favorite?.CategoryId ?? 1;
            var items = await _uow.Mangas.GetMangaByCategoryAsync(categoryId);
            var pick = items.Take(top).ToList();
            var prices = await Task.WhenAll(pick.Select(m => _uow.PriceHistory.GetCurrentPriceAsync(m.Id)));
            return pick.Select((m, idx) => new MangaDto
            {
                Id = m.Id,
                Title = m.Title,
                Stock = m.Stock,
                Synopsis = m.Synopsis,
                ImageUrl = m.ImageUrl,
                CategoryId = m.CategoryId,
                CategoryName = m.Category?.Name,
                AuthorId = m.AuthorId,
                AuthorName = m.Author?.Name,
                CurrentPrice = prices[idx]
            }).ToList();
        }
    }
}
