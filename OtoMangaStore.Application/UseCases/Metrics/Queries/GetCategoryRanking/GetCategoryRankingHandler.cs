using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Metrics.Queries.GetCategoryRanking
{
    public class GetCategoryRankingHandler : IRequestHandler<GetCategoryRankingQuery, IReadOnlyList<CategoryRankingDto>>
    {
        private readonly IClickMetricsRepository _repo;

        public GetCategoryRankingHandler(IClickMetricsRepository repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<CategoryRankingDto>> Handle(GetCategoryRankingQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetCategoryRankingAsync();
        }
    }
}
