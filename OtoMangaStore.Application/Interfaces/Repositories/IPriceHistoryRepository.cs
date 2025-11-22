using System.Threading.Tasks;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    public interface IPriceHistoryRepository
    {
        Task<decimal> GetCurrentPriceAsync(int mangaId);
    }
}