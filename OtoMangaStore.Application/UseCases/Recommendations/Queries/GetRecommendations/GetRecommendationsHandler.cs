using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Recommendations.Queries.GetRecommendations
{
    public class GetRecommendationsHandler : IRequestHandler<GetRecommendationsQuery, IReadOnlyList<MangaDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetRecommendationsHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<MangaDto>> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
        {
            var ranking = await _uow.ClickMetrics.GetCategoryRankingAsync();
            var favorite = ranking.OrderByDescending(x => x.Clicks).FirstOrDefault();
            var categoryId = favorite?.CategoryId ?? 1;
            var items = await _uow.Mangas.GetMangaByCategoryAsync(categoryId);
            var pick = items.Take(request.Top).ToList();
            var prices = await Task.WhenAll(pick.Select(m => _uow.PriceHistory.GetCurrentPriceAsync(m.Id)));
            return pick.Select((m, idx) => new MangaDto
            {
                Id = m.Id,
                Title = m.Title,
                Stock = m.Stock,
                Synopsis = m.Synopsis,
                ImageUrl = m.ImageUrl,
                CategoryId = m.CategoryId,
                CategoryName = m.Category?.Name ?? string.Empty,
                AuthorId = m.AuthorId,
                AuthorName = m.Author?.Name ?? string.Empty,
                CurrentPrice = prices[idx]
            }).ToList();
        }
    }
}
