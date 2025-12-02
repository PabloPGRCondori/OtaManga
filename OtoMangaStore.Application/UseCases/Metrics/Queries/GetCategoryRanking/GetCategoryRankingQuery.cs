using MediatR;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.UseCases.Metrics.Queries.GetCategoryRanking
{
    public class GetCategoryRankingQuery : IRequest<IReadOnlyList<CategoryRankingDto>>
    {
    }
}
