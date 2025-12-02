using System.Threading.Tasks;
using System.Collections.Generic;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    public interface IPriceHistoryRepository
    {
        Task<decimal> GetCurrentPriceAsync(int mangaId);
        Task AddAsync(PriceHistory priceHistory);
        Task<IEnumerable<PriceHistory>> GetHistoryByMangaIdAsync(int mangaId);
        Task<Dictionary<int, decimal>> GetCurrentPricesForMangasAsync(IEnumerable<int> mangaIds);
    }
}