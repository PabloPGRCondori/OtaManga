using MediatR;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.UseCases.Recommendations.Queries.GetRecommendations
{
    public class GetRecommendationsQuery : IRequest<IReadOnlyList<MangaDto>>
    {
        public string UserId { get; }
        public int Top { get; }

        public GetRecommendationsQuery(string userId, int top)
        {
            UserId = userId;
            Top = top;
        }
    }
}
