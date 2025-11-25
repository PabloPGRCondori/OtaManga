using System.Collections.Generic;
using System.Threading.Tasks;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.Interfaces.Services
{
    public interface IRecommendationService
    {
        Task<IReadOnlyList<MangaDto>> GetRecommendationsAsync(string userId, int top);
    }
}
